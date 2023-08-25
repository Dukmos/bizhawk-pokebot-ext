using bizhawk.pokebot.ext.Data;
using bizhawk.pokebot.ext.Utils;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Emulation.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BizHawk.PokeBot.ext
{
    [ExternalTool("PokeBot")]
    public sealed partial class PokeBot : ToolFormBase, IExternalToolForm
    {
        public ApiContainer? _maybeAPIContainer { get; set; }
        private ApiContainer APIs => _maybeAPIContainer!;
        private MemoryMappedFiles MMF => APIs.Comm.MMF;
        protected override string WindowTitleStatic => "PokeBot";

        public PokeBot() => InitializeComponent();

        public static Dictionary<string, object> input;

        private void GetInput() => input = APIs.Joypad.Get().ToDictionary(pair => pair.Key, pair => pair.Value);

        private void SendInputs() => APIs.Joypad.Set(input.ToDictionary(pair => pair.Key, pair => Convert.ToBoolean(pair.Value)));

        private bool restarting = false;

        public override void Restart()
        {
            restarting = true;
            Memory.Initialize(APIs.Memory);
            GameSettings.Initialize(APIs.Memory);
            Emulator.Initialize(APIs);
            LB_GameName.Text = "Game: " + GameSettings.gamename;

            GetInput();

            if (enable_input)
            {
                input.ToDictionary(pair => pair.Key, pair => pair.Value is int ? pair.Value : false);
                input["SaveRAM"] = false;
                input["Screenshot"] = false;
                SendInputs();
            }

            //Allocate memory mapped file sizes
            MMF.WriteToFile("bizhawk_screenshot-" + bot_instance_id, new string('\x00', 24576));
            MMF.Filename = "bizhawk_screenshot-" + bot_instance_id;
            MMF.ScreenShotToFile();

            MMF.WriteToFile("bizhawk_screenshot-" + bot_instance_id, new string('\x00', 4096));
            MMF.WriteToFile("bizhawk_hold_input-" + bot_instance_id, new string('\x00', 4096));
            MMF.WriteToFile("bizhawk_trainer_data-" + bot_instance_id, new string('\x00', 4096));
            MMF.WriteToFile("bizhawk_bag_data-" + bot_instance_id, new string('\x00', 8192));
            MMF.WriteToFile("bizhawk_party_data-" + bot_instance_id, new string('\x00', 8192));
            MMF.WriteToFile("bizhawk_opponent_data-" + bot_instance_id, new string('\x00', 4096));
            MMF.WriteToFile("bizhawk_emu_data-" + bot_instance_id, new string('\x00', 4096));

            //101 entries, the final entry is for the index.
            byte[] input_list = Enumerable.Repeat(Convert.ToByte('a'), 101).ToArray();

            //Create memory mapped input files for Python script to write to
            MMF.WriteToFile("bizhawk_hold_input-" + bot_instance_id, JsonConvert.SerializeObject(input) + '\x00');
            MMF.WriteToFile("bizhawk_input_list-" + bot_instance_id, new string('\x00', 4096));
            MMF.WriteToFile("bizhawk_input_list-" + bot_instance_id, input_list);

            mainLoop();
            restarting = false;
        }

        private const int NUM_OF_FRAMES_PER_PRESS = 5;

        private Emulator emu_data = new Emulator();

        protected override void UpdateAfter()
        {
            if (restarting) return;
            if (CB_coords.Checked) Update_Coords();

            if (emu_data.frameCount % NUM_OF_FRAMES_PER_PRESS == 0)
            {
                string[] key = new string[input.Keys.Count];
                input.Keys.CopyTo(key, 0);
                foreach (var i in key)
                {
                    input[i] = false;
                }
                SendInputs();
            }
            else traverseNewInputes();

            handleHeldButtons();

            //Save screenshot and other data to memory mapped files, as fps is higher, reduce the number of reads and writes to memory
            MMF.WriteToFile("bizhawk_emu_data-" + bot_instance_id, JsonConvert.SerializeObject(new Dictionary<string, object>() { { "emu", emu_data } }) + '\x00');
            int fps = emu_data.fps;
            if ((fps > 120) || fps <= 240) //Copy screenshot to memory every nth frame if running at higher than 1x
            {
                if (emu_data.frameCount % 2 == 0) mainLoop();
            }
            else if ((fps > 240) || fps <= 480) //Copy screenshot to memory every nth frame if running at higher than 1x
            {
                if (emu_data.frameCount % 3 == 0) mainLoop();
            }
            else if (fps > 480)
            {
                if (emu_data.frameCount % 4 == 0) mainLoop();
            }
            else mainLoop();
        }

        #region PokeBot

        private string bot_instance_id => TB_BotInstanceID.Text;
        private bool enable_input => CB_EnableInput.Checked;

        private Trainer trainer;

        public void mainLoop()
        {
            trainer = new Trainer();
            MMF.WriteToFile("bizhawk_trainer_data-" + bot_instance_id, JsonConvert.SerializeObject(new Dictionary<string, object>() { { "trainer", trainer } }) + '\x00');
            MMF.WriteToFile("bizhawk_party_data-" + bot_instance_id, JsonConvert.SerializeObject(new Dictionary<string, object>() { { "party", Party.getParty() } }) + '\x00');
            MMF.WriteToFile("bizhawk_opponent_data-" + bot_instance_id, JsonConvert.SerializeObject(new Dictionary<string, object>() { { "opponent", new Pokemon(GameSettings.estats) } }) + '\x00');
            MMF.WriteToFile("bizhawk_bag_data-" + bot_instance_id, JsonConvert.SerializeObject(new Dictionary<string, object>() { { "bag", Bag.getBag() } }) + '\x00');

            //TODO: Implement WriteFiles

            MMF.ScreenShotToFile();
        }

        private int g_current_index = 0; //Keep track of where Lua is in it's traversal of the input list

        public void traverseNewInputes()
        {
            string list_of_inputs;
            try
            {
                list_of_inputs = MMF.ReadFromFile("bizhawk_input_list-" + bot_instance_id, 4096);
                //list_of_inputs = MMF.ReadBytesFromFile("bizhawk_input_list-" + bot_instance_id, 4096);
            }
            catch
            {
                APIs.Gui.AddMessage("error recieving Inputs");
                return;
            }

            int current_index = g_current_index;
            byte python_current_index = Convert.ToByte(list_of_inputs[100]);
            python_current_index--;
            if (current_index != python_current_index)
            {
                while (current_index != python_current_index)
                {
                    current_index += 1;
                    if (current_index > 100) current_index = 0;
                    string button = Convert.ToChar(Convert.ToByte(list_of_inputs[current_index])).ToString();
                    //APIs.Gui.AddMessage(button);
                    if (button == "l") button = "Left";
                    if (button == "r") button = "Right";
                    if (button == "u") button = "Up";
                    if (button == "d") button = "Down";
                    if (button == "s") button = "Select";
                    if (button == "S") button = "Start";
                    input[button] = true;
                    if (button == "A") input["B"] = false; //If there are any new "A" presses after "B" in the list, discard the "B" presses before it

                    if (button == "x")
                    {
                        APIs.EmuClient.SaveRam();
                        APIs.Gui.AddMessage("SaveRAM flushed!");
                    }
                }
            }

            g_current_index = current_index;

            if (enable_input) SendInputs();
        }

        private void handleHeldButtons()
        {
            Dictionary<string, object> hold_result;
            string mmfReturn;
            try
            {
                mmfReturn = MMF.ReadFromFile("bizhawk_hold_input-" + bot_instance_id, 4096);
                mmfReturn = mmfReturn.Remove(mmfReturn.IndexOf('}') + 1);
                hold_result = JsonConvert.DeserializeObject<Dictionary<string, object>>(mmfReturn);
            }
            catch (Exception e)
            {
                APIs.Gui.AddMessage("decode HeldButtons Failed");
                return;
            }
            foreach (var b in hold_result.Where(pair => pair.Value is bool).Select(pair => new { button = pair.Key, button_is_held = Convert.ToBoolean(pair.Value) }))
            {
                if (b.button_is_held) input[b.button] = true;
                //Don't assign them false, this function is called after the presses and would overwrite them to false
            }
            if (last_state != trainer.state) last_state = trainer.state;

            if (enable_input) SendInputs();
        }

        //TODO

        #endregion PokeBot

        #region Update Coords

        private uint last_posX = 0;
        private uint last_posY = 0;
        private uint last_state = 0;
        private uint last_mapBank = 0;
        private uint last_mapId = 0;

        private void Update_Coords()
        {
            Trainer trainer = new();

            if (last_state != trainer.state)
            {
                last_state = trainer.state;
                LB_State.Text = string.Format("State: {0}", trainer.state);
            }

            if ((last_posX != trainer.posX) || (last_posY != trainer.posY))
            {
                last_posX = trainer.posX;
                last_posY = trainer.posY;
                LB_XY.Text = string.Format("X:{0}, Y:{1}", trainer.posX, trainer.posY);
            }

            if ((last_mapBank != trainer.mapBank) || (last_mapId != trainer.mapId))
            {
                last_mapBank = trainer.mapBank;
                last_mapId = trainer.mapId;
                LB_Map.Text = string.Format("Map: {0}:{1}", trainer.mapBank, trainer.mapId);
            }
        }

        private void CB_coords_CheckedChanged(object sender, System.EventArgs e)
        {
            GB_coords.Visible = CB_coords.Checked;
        }

        #endregion Update Coords
    }
}