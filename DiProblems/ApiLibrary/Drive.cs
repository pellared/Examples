using Pellared.Owned;

namespace ApiLibrary
{
    public interface ISpeed
    {
        int KilometeresPerHour { get; set; }
    }

    // This is an API class that cannot change (used by other vendors, projects etc.)
    // Additionaly we do not want to expose the IMotor interface which is an implementation detail
    public class Drive
    {
        private readonly IMotor _motor;
        private readonly ISpeed _speed;

        public Drive()
            : this(Bootstrapper.Get<ISpeed>())
        { }

        public Drive(ISpeed speed)
        {
            _speed = speed;
            _motor = Bootstrapper.Get<IFactory<IMotor>>().Create().Value;
        }

        public void StartMotor()
        {
            _motor.Start(_speed);
        }

        public void StopMotor()
        {
            _motor.Stop();
        }


        public int MotorSpeed
        {
            get { return _speed.KilometeresPerHour; }
            set { _speed.KilometeresPerHour = value; }
        }
    }
}
