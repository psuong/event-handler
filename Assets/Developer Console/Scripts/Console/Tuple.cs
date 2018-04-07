﻿namespace DeveloperConsole.Utility {

    /// <summary>
    /// A structured pair of elements.
    /// </summary>
    public struct Tuple<T1, T2> {
        
        public T1 first;
        public T2 second;
        
        /// <summary>
        /// Constructor for the tuple, an ordered pair of elements.
        /// </summary>
        public Tuple(T1 first, T2 second) {
            this.first = first;
            this.second = second;
        }
        
        /// <summary>
        /// A utility function to construct a tuple, similar to the constructor.
        /// </summary>
        public static Tuple<T1, T2> Create(T1 first, T2 second) {
            return new Tuple<T1, T2>(first, second);
        }
    }

    public struct Tuple<T1, T2, T3> {
        public T1 first;
        public T2 second;
        public T3 third;
        
        /// <summary>
        /// Constructor for the 3 tuple, a triple of ordered elements.
        /// </summary>
        public Tuple(T1 first, T2 second, T3 third) {
            this.first = first;
            this.second = second;
            this.third = third;
        }
        
        /// <summary>
        /// A utility function to construct a 3 tuple, similar to the constructor.
        /// </summary>
        public static Tuple<T1, T2, T3> Create(T1 first, T2 second, T3 third) {
            return new Tuple<T1, T2, T3>(first, second, third);
        }
    }
}