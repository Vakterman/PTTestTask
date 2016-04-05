using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;

namespace FibonacciWebApi.Controllers
{
    public class FibonacciController : ApiController
    {
        private IFibonacciLogicFacade<FibonacciOperation> _facadeLogic;
        public FibonacciController(IFibonacciLogicFacade<FibonacciOperation> facadeLogic)
        {
            if (facadeLogic == null)
            {
                throw new ArgumentNullException("facadeLogic");
            }

            _facadeLogic = facadeLogic;
        }

        public void Put([FromBody]FibonacciOperation fibonaccyOperation)
        {
            _facadeLogic.Evaluate(fibonaccyOperation);
        }
    }
}
