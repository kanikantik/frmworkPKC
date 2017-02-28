// --------------------------------------------------------------------------------------------------------------------
// <copyright company="EPAM Systems" file="GlimpseSecurityPolicy.cs">
//   Copyright 2015
// </copyright>
// <summary>
//   Glimpse security policy
// </summary> 
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Web
{
    using Glimpse.AspNet.Extensions;
    using Glimpse.Core.Extensibility;

    /// <summary>
    /// The glimpse security policy.
    /// </summary>
    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        /// <summary>
        /// Gets the execute on.
        /// </summary>
        /// <value>
        /// The execute on.
        /// </value>
        public RuntimeEvent ExecuteOn
        {
            // The RuntimeEvent.ExecuteResource is only needed in case you create a security policy
            // Have a look at http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ for more details
            get { return RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource; }
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="policyContext">The policy context.</param>
        /// <returns>
        /// The <see cref="RuntimePolicy" />.
        /// </returns>
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            // You can perform a check like the one below to control Glimpse's permissions within your application.
            // More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy            

            return RuntimePolicy.On;
        }

    }
}