// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeansImplicitUseAttribute.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the MeansImplicitUseAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Annotations
{
    using System;

    /// <summary>
    /// Should be used on attributes and causes ReSharper
    /// to not mark symbols marked with such attributes as unused
    /// (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class MeansImplicitUseAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTarget.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">The use kind flags.</param>
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTarget.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        /// <param name="targetFlags">The target flags.</param>
        public MeansImplicitUseAttribute(ImplicitUseTarget targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">The use kind flags.</param>
        /// <param name="targetFlags">The target flags.</param>
        public MeansImplicitUseAttribute(
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
        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }

        /// <summary>
        /// Gets the target flags.
        /// </summary>
        /// <value>
        /// The target flags.
        /// </value>
        [UsedImplicitly]
        public ImplicitUseTarget TargetFlags { get; private set; }
    }
}