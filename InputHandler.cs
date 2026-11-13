using PassiveSkillTreePlanner;
using ExileCore;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExileCore.Shared;

namespace PassiveSkillTreePlanner
{
    public class InputHandler
    {
        private readonly Vector2 _windowPosition;
        private readonly PassiveSkillTreePlannerSettings _settings;

        public InputHandler(Vector2 windowPosition, PassiveSkillTreePlannerSettings settings)
        {
            _windowPosition = windowPosition;
            _settings = settings;
        }

        public async Task MoveCursorAndClick(Vector2 position, CancellationToken cancellationToken)
        {
            try
            {
                // Ensure we don't click while token is cancelled
                cancellationToken.ThrowIfCancellationRequested();

                // Move cursor to desired position (offset by window)
                var targetPos = position + _windowPosition;
                Input.SetCursorPos(targetPos);

                // Wait a short moment for the cursor to stabilize
                await Task.Delay(_settings.InputDelay + 50, cancellationToken);

                // Safety: check again before clicking
                cancellationToken.ThrowIfCancellationRequested();

                // Perform the left click
                Input.Click(MouseButtons.Left);

                // Small delay after click to give UI time to respond
                await Task.Delay(120, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // fine — do nothing if cancelled
            }
            
        }
        /// <summary>
        /// Ctrl+click as one uninterruptible unit: no cancellation token, modifier always released,
        /// and the click is skipped if <paramref name="canClick"/> says the screen changed (e.g. zone load).
        /// </summary>
        public async Task<bool> CtrlClickAtomic(Vector2 position, Func<bool> canClick)
        {
            try
            {
                Input.KeyDown(Keys.LControlKey);
                Input.SetCursorPos(position + _windowPosition);
                await Task.Delay(_settings.InputDelay + 50);

                if (canClick != null && !canClick())
                    return false;

                Input.Click(MouseButtons.Left);
                await Task.Delay(_settings.InputDelay);
                return true;
            }
            finally
            {
                Input.KeyUp(Keys.LControlKey);
            }
        }

         public async Task MoveCursorAndControlClick(Vector2 position, CancellationToken cancellationToken)
        {
            try
            {
                // Ensure we don't click while token is cancelled
                cancellationToken.ThrowIfCancellationRequested();

                // Move cursor to desired position (offset by window)
                var targetPos = position + _windowPosition;
                Input.SetCursorPos(targetPos);

                // Wait a short moment for the cursor to stabilize
                await Task.Delay(_settings.InputDelay + 50, cancellationToken);

                // Safety: check again before clicking
                cancellationToken.ThrowIfCancellationRequested();
                Input.KeyDown(Keys.LControlKey);
                await Task.Delay(_settings.InputDelay);
                // Perform the left click
                Input.Click(MouseButtons.Left);
                await Task.Delay(_settings.InputDelay);

                Input.KeyUp(Keys.LControlKey);
                await Task.Delay(_settings.InputDelay);

                // Small delay after click to give UI time to respond
                await Task.Delay(120, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // fine — do nothing if cancelled
            }
            
        }


        public async Task MoveCursorAndRightClick(Vector2 position, CancellationToken cancellationToken)
        {
            Input.SetCursorPos(position + _windowPosition);
            await Task.Delay(_settings.InputDelay, cancellationToken);
            Input.Click(MouseButtons.Right);
        }
        public async Task MoveCursor(Vector2 position, CancellationToken cancellationToken)
        {
            Input.SetCursorPos(position + _windowPosition);
            await Task.Delay(_settings.InputDelay, cancellationToken);
            
        }

        public async Task PressControlKey(Func<Task> action, CancellationToken cancellationToken)
        {
            try
            {
                Input.KeyDown(Keys.LControlKey);
                await Task.Delay(_settings.InputDelay, cancellationToken);
                await action();
            }
            finally
            {
                Input.KeyUp(Keys.LControlKey);
            }
        }        
        
        public async Task PressEnterKey( CancellationToken cancellationToken)
        {
            try
            {
                Input.KeyDown(Keys.Enter);
                await Task.Delay(_settings.InputDelay, cancellationToken);
  
            }
            finally
            {
                Input.KeyUp(Keys.Enter);
            }
        }

        public async Task PressKey(CancellationToken cancellationToken)
        {
            try
            {
                Input.KeyDown(Keys.Enter);
                await Task.Delay(_settings.InputDelay, cancellationToken);

            }
            finally
            {
                Input.KeyUp(Keys.Enter);
            }
        }
    }
}
