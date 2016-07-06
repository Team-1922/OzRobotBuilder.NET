using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    /// <summary>
    /// used in <see cref="UniqueItemList{T}"/> as a way to recognize uniqueness
    /// </summary>
    public interface INamedClass
    {
        /// <summary>
        /// </summary>
        /// <returns>The name of the class</returns>
        string GetName();
        /// <summary>
        /// set the name of the instance
        /// </summary>
        /// <param name="name">the name to set</param>
        void SetName(string name);
    }
    /// <summary>
    /// An extension to the <see cref="List{T}"/> class enforcing each item to have a unique "name" attribute
    /// </summary>
    /// <typeparam name="T">type of object</typeparam>
    public class UniqueItemList<T> : List<T> where T : INamedClass
    {
        /// <summary>
        /// Add a new item and overwriting its name if not unique
        /// </summary>
        /// <param name="newItem">the item to add</param>
        public new virtual void Add(T newItem)
        {
            foreach(var itm in this)
            {
                if (newItem.GetName() == itm.GetName())
                {
                    for(int appendCount = 0; appendCount < 100; appendCount++)
                    {
                        bool shouldContinue = false;
                        foreach(var itm0 in this)
                        {
                            //if any of the items have this name, do continue
                            if(itm0.GetName() == newItem.GetName() + appendCount)
                            {
                                shouldContinue = true;
                            }
                        }
                        if (!shouldContinue)
                        {
                            newItem.SetName(newItem.GetName() + appendCount);
                            base.Add(newItem);
                            break;
                        }
                    }
                    return;//well if we get here something weird happened
                }
            }
            base.Add(newItem);
        }
    }
}
