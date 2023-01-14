using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Users : INotifyPropertyChanged
{
    private string? name;
    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Name")); }
        }

    }
    private string? password;
    public string? Password
    {
        get { return password; }
        set
        {
            password = value;
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Password")); }
        }

    }
    public override string ToString() => ToStringExtension.ToStringProperty(this);

    public event PropertyChangedEventHandler? PropertyChanged;
}
 