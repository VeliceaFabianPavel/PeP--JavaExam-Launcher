﻿using System;
using System.Collections.Generic;

namespace JavaExam;

public partial class Proctor
{
    public int ProctorId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();

    public virtual ICollection<Studenti> Studentis { get; set; } = new List<Studenti>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
