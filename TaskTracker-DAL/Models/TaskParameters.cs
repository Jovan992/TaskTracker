using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_DAL.Models;

public class TaskParameters : QueryStringParameters
{
    public int? ProjectId { get; set; } = null;
}
