using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Owned
{
    public class Finalizer
    {
        private readonly Action onFinalize;

        public Finalizer(Action onFinalize)
        {
            this.onFinalize = onFinalize;
        }

        ~Finalizer()
        {
            onFinalize();
            GC.SuppressFinalize(this);
        }

        public void SuppressFinalize()
        {
            GC.SuppressFinalize(this);
        }
    }
}
