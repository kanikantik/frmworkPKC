// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsedImplicitlyAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the UsedImplicitlyAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Indicates that the marked symbol is used implicitly
    /// (e.g. via reflection, in external library), so this symbol
    /// will not be marked as unused (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class UsedImplicitlyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTarget.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">The use kind flags.</param>
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTarget.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        /// <param name="targetFlags">The target flags.</param>
        public UsedImplicitlyAttribute(ImplicitUseTarget targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">The use kind flags.</param>
        /// <param name="targetFlags">The target flags.</param>
        public UsedImplicitlyAttribute(
            ImplicitUseKindFlags useKindFlags, ImplicitUseTarget targetFlags)
        {
            this.UseKindFlags = useKindFlags;
            this.TargetFlags = targetFlags;
        }

        /// <summary>
        /// Gets the use kind flags.
        /// </summary>
        /// <value>
        /// The use kind flags.
        /// </value>
        public ImplicitUseKindFlags UseKindFlags { get; private set; }

        /// <summary>
        /// Gets the target flags.
        /// </summary>
        /// <value>
        /// The target flags.
        /// </value>
        public ImplicitUseTarget TargetFlags { get; private set; }
    }
}