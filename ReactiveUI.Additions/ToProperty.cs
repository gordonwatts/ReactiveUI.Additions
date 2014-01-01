
using System;
namespace ReactiveUI
{
    /// <summary>
    /// Additions to ToProperty that are independent of ReactiveObject is some way
    /// </summary>
    public static class ToPropertyHelpers
    {
        /// <summary>
        /// Represents a read only backing property. Put it in the getter for your
        /// function. Disposing of it will kill off the subscription.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class BackingPropertyRO<T> : IDisposable
        {
            /// <summary>
            /// Initialize a backing property from an observable source.
            /// Whatever scheduler this happens on the Value will be set on.
            /// </summary>
            /// <param name="source">The source of property values</param>
            /// <param name="initialValue">The initial value for the backing value</param>
            public BackingPropertyRO(IObservable<T> source, T initialValue = default(T))
            {
                _subscription = source.Subscribe(
                    x => { Value = x; },
                    err => { Value = initialValue; }
                    );
                Value = initialValue;
            }

            /// <summary>
            /// Track the subscription for later disposal.
            /// </summary>
            IDisposable _subscription;

            /// <summary>
            /// Returns the current value of the property.
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// Clean up the observable subscription when we are disposed!
            /// </summary>
            public void Dispose()
            {
                _subscription.Dispose();
            }
        }

        /// <summary>
        /// The simplest of possible property conversions. Will send the result of
        /// a stream of values to a value. The values will be set on the thread they
        /// arrive on.
        /// </summary>
        /// <typeparam name="T">The type of the observable</typeparam>
        /// <param name="propertySource">The observable source for the property</param>
        /// <param name="initialValue">Initial value for the observable</param>
        /// <returns>Returns a read only backing object. Put it in the getter of your property - bc.Value.</returns>
        /// <remarks>
        /// After the sequence completes the value will keep its last value
        /// If an exception comes down the road the value will keep its initial value, and
        /// no exception will be thrown.
        /// </remarks>
        public static BackingPropertyRO<T> ToProperty<T>(this IObservable<T> propertySource,
            T initialValue = default(T))
        {
            var bc = new BackingPropertyRO<T>(propertySource, initialValue);
            return bc;
        }
    }
}
