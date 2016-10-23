using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for command group viewmodels
    /// </summary>
    public interface ICommandGroupProvider : IProvider<CommandGroup>
    {
        /// <summary>
        /// The command group items for this command group
        /// </summary>
        IObservableCollection<ICommandGroupItemProvider> Commands { get; }

        /// <summary>
        /// Inserts a command group item into this command group
        /// </summary>
        /// <param name="item">the item to insert</param>
        /// <param name="index">the location in the list to insert it</param>
        void InsertCommandGroupItem(CommandGroupItem item, int index);
        /// <summary>
        /// Removes the command group item at a given index
        /// </summary>
        /// <param name="index">the index at which to remove the command group item</param>
        void RemoveCommandGroupItem(int index);
    }
}
