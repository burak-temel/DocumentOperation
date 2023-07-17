using Castle.DynamicProxy;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentOperation.Core
{
    public class GeneralInterceptor : IInterceptor
    {
        //Entegre edemedim amaç metodların şlenmesi sırasında kontrol sağlamak
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                // OnBefore section
                Log.Information($"Before executing method: {invocation.Method.Name}");

                // Proceed with the original method execution
                invocation.Proceed();

                // OnSuccess section
                Log.Information($"Method executed successfully: {invocation.Method.Name}");
            }
            catch (Exception ex)
            {
                OnException(invocation);
                // OnException section
                Log.Error(ex, $"Exception occurred in method: {invocation.Method.Name}");

                // Re-throw the exception to propagate it further
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
                // OnAfter section
            }

            OnAfter(invocation);
            Log.Information($"After executing method: {invocation.Method.Name}");
        }
    }

}
