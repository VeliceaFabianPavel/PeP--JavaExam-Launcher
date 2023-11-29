﻿using System;
using System.Collections.Generic;

namespace JavaExam;

public partial class TestQuestion
{
    public int TestQuestionId { get; set; }

    public int? TestId { get; set; }

    public int? QuestionId { get; set; }

    public string? StudentAnswer { get; set; }

    public virtual QuestionBank? Question { get; set; }

    public virtual Test? Test { get; set; }
}
