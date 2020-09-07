using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Tests.General
{
    public class BaseTest
    {
        #region protect

        protected ServiceProvider _kernal;

        #endregion

        #region construrcto

        public BaseTest()
        {
            _kernal = new Kernal().KERNAL;    
        }

        #endregion

        #region public method

        public void ReCreateKernal()
        {
            _kernal = new Kernal().KERNAL;
        }

        #endregion
    }
}
