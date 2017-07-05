using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic
{
    /// <summary>
    /// Shared interface for the input of both logics.
    /// </summary>
    public interface InputManager { IOption<Point> Click(); }
}