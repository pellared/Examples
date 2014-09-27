using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    public class DriveBuilder
    {
        private ISpeed _speed;

        public DriveBuilder WithSpeed(ISpeed speed)
        {
            _speed = speed;
            return this;
        }

        public Drive Build()
        {
            if (_speed == null)
            {
                _speed = Bootstrapper.Get<ISpeed>();
            }

            return new Drive(_speed);
        }
    }
}
