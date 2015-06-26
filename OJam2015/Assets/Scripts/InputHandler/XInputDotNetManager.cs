using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace InputHandler
{
    public class XInputDotNetManager : InputManager
    {
        private bool[] _initialSetupDone;
        private PlayerIndex[] _playerIndexes;
        private GamePadState[] _gamePadPreviousStates;
        private GamePadState[] _gamePadStates;

        protected override void InitialSetup()
        {
            _initialSetupDone = new bool[MAX_PLAYER_COUNT];
            _playerIndexes = new PlayerIndex[MAX_PLAYER_COUNT];
            _gamePadPreviousStates = new GamePadState[MAX_PLAYER_COUNT];
            _gamePadStates = new GamePadState[MAX_PLAYER_COUNT];

            for (int i = 0; i < MAX_PLAYER_COUNT; i++)
            {
                _gamePadStates[i] = GamePad.GetState(_playerIndexes[i]);
            }
        }

        protected override void MapInputs()
        {
            for (int i = 0; i < MAX_PLAYER_COUNT; i++)
            {
                _gamePadPreviousStates[i] = _gamePadStates[i];
                _gamePadStates[i] = GamePad.GetState(_playerIndexes[i]);

                if (!_gamePadPreviousStates[i].IsConnected || !_initialSetupDone[i])
                {
                    _initialSetupDone[i] = true;

                    if (_gamePadStates[i].IsConnected)
                    {
                        _playerIndexes[i] = (PlayerIndex)i;

                        Debug.Log(string.Format("GamePad {0} is ready", _playerIndexes[i]));
                    }
                }

                MapPlayerInput(_inputMappers[i], _gamePadStates[i], _gamePadPreviousStates[i]);
            }
        }

        // TODO: Maybe reduce it to only the inputs actually used in the game?
        private void MapPlayerInput(InputMapper inputMapper, GamePadState state, GamePadState previousState)
        {
            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.A,
                state.Buttons.A == ButtonState.Pressed,
                previousState.Buttons.A == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.B,
                state.Buttons.B == ButtonState.Pressed,
                previousState.Buttons.B == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.X,
                state.Buttons.X == ButtonState.Pressed,
                previousState.Buttons.X == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.Y,
                state.Buttons.Y == ButtonState.Pressed,
                previousState.Buttons.Y == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.Back,
                state.Buttons.Back == ButtonState.Pressed,
                previousState.Buttons.Back == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.Start,
                state.Buttons.Start == ButtonState.Pressed,
                previousState.Buttons.Start == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.LeftShoulder,
                state.Buttons.LeftShoulder == ButtonState.Pressed,
                previousState.Buttons.LeftShoulder == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.RightShoulder,
                state.Buttons.RightShoulder == ButtonState.Pressed,
                previousState.Buttons.RightShoulder == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.DPadLeft,
                state.DPad.Left == ButtonState.Pressed,
                previousState.DPad.Left == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.DPadRight,
                state.DPad.Right == ButtonState.Pressed,
                previousState.DPad.Right == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.DPadDown,
                state.DPad.Down == ButtonState.Pressed,
                previousState.DPad.Down == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.DPadUp,
                state.DPad.Up == ButtonState.Pressed,
                previousState.DPad.Up == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.LeftStick,
                state.Buttons.LeftStick == ButtonState.Pressed,
                previousState.Buttons.LeftStick == ButtonState.Pressed
            );

            inputMapper.SetRawButtonState(
                (int)XboxInputConstants.Buttons.RightStick,
                state.Buttons.RightStick == ButtonState.Pressed,
                previousState.Buttons.RightStick == ButtonState.Pressed
            );

            inputMapper.SetRawAxisValue((int)XboxInputConstants.Axis.LeftStickX, state.ThumbSticks.Left.X);
            inputMapper.SetRawAxisValue((int)XboxInputConstants.Axis.LeftStickY, state.ThumbSticks.Left.Y);
            inputMapper.SetRawAxisValue((int)XboxInputConstants.Axis.RightStickX, state.ThumbSticks.Right.X);
            inputMapper.SetRawAxisValue((int)XboxInputConstants.Axis.RightStickY, state.ThumbSticks.Right.Y);
            inputMapper.SetRawAxisValue((int)XboxInputConstants.Axis.TriggerLeft, state.Triggers.Left);
            inputMapper.SetRawAxisValue((int)XboxInputConstants.Axis.TriggerLeft, state.Triggers.Right);
        }
    }
}
