// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
	public class NoteDto: ModelBase
	{
		public string Text { get; set; }
		public DateTime Created { get; set; }
	}
}