using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Customer
{
    public int m_OrderId { get; set; }

    public int m_ID { get; set; }

    public string? Name { get; set; }

    public int Password { get; set; }

    public BO.Cart m_Cart { get; set; }
}
