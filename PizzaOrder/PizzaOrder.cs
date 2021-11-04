/* Bill Nicholson
 * nicholdw@ucmail.uc.edu
 */
using System;
using System.Collections.Generic;

namespace PizzaOrderNamespace {
    public class PizzaOrder {
        private List<String> mToppings;
        private String mCrust;
        public enum enumPizzaSize { slice, personal, small, medium, large, extraLarge };
        private enumPizzaSize mPizzaSize;

        public PizzaOrder()  { }

        public enumPizzaSize pizzaSize {
            get { return mPizzaSize; }
            set { mPizzaSize = value; }
        }

        /// <summary>
        /// Crust of the pizza
        /// </summary>
        public String crust {
            get { return mCrust; }
            set { mCrust = value; }
        }
        /// <summary>
        /// Toppings list for this pizza
        /// </summary>
        public List<String> toppings {
            get { return new List<String>(mToppings); }
            set { mToppings = new List<String>(value); }  
        }
        /// <summary>
        /// Convert current PizzaOrder to a string representation
        /// </summary>
        /// <returns></returns>
        public override String ToString() {
            return crust + ": " + String.Join(", ", mToppings.ToArray());  // This needs work
        }
    }
}
