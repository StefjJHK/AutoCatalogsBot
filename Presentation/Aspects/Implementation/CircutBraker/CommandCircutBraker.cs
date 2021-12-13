using Google;
using Microsoft.Extensions.Logging;
using Presentation.Aspects.Abstracts;
using Presentation.ParametrObjects;
using System;
using System.Timers;
using static Presentation.Aspects.CircutBraker.CircutBrackerStates;

namespace Presentation.Aspects.Implementation.CircutBraker
{
    public class CommandCircutBraker : ICommandCircutBraker
    {
        private State circutState = State.Closed;
        private State _circutState
        {
            get
            {
                return circutState;
            }
            set
            {
                _logger.LogDebug("State = {State}", Enum.GetName(value));
                circutState = value;
            }
        }

        private readonly Timer _timer;

        private readonly ILogger<RequestCircutBraker> _logger;

        public CommandCircutBraker(CircutBrakerParams brakerParams, ILogger<RequestCircutBraker> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _timer = new Timer(brakerParams.TimeOutInterval);
            _timer.Elapsed += TimerTimeOut;
        }

        public void Execute(Action action)
        {
            if (_circutState == State.Open)
            {
                throw new CommandCircutBreakerException("Circut is currently open", action.GetType());
            }

            try
            {
                action.Invoke();

                Succeed();
            }
            catch (GoogleApiException exception)
            {
                Trip();
                throw;
            }
        }

        private void Trip()
        {
            if (_circutState == State.Closed || _circutState == State.HalfOpen)
            {
                _circutState = State.Open;
            }

            _timer.Start();
        }

        private void TimerTimeOut(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _circutState = State.HalfOpen;
        }

        private void Succeed()
        {
            if (_circutState == State.HalfOpen)
            {
                _circutState = State.Closed;
            }
        }
    }

    public class CommandCircutBreakerException : Exception
    {
        public Type CommandType { get; init; }

        public CommandCircutBreakerException(string message, Type commandType) : base(message)
        {
            CommandType = commandType;
        }
    }
}
