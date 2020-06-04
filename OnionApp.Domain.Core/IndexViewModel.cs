using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.Domain.Core
{
   public class IndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
