// /***************************************************************************
//  * MP3SharpException.cs
//  * Copyright (c) 2015, 2021 The Authors.
//  * 
//  * All rights reserved. This program and the accompanying materials
//  * are made available under the terms of the GNU Lesser General Public License
//  * (LGPL) version 3 which accompanies this distribution, and is available at
//  * https://www.gnu.org/licenses/lgpl-3.0.en.html
//  *
//  * This library is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  ***************************************************************************/

using System;
using System.IO;
using MP3Sharp.Support;

namespace MP3Sharp {
    /// <summary>
    /// MP3SharpException is the base class for all API-level
    /// exceptions thrown by MP3Sharp. To facilitate conversion and
    /// common handling of exceptions from other domains, the class
    /// can delegate some functionality to a contained Throwable instance.
    /// </summary>
    public class MP3SharpException : Exception {
        internal MP3SharpException() { }

        internal MP3SharpException(string message) : base(message) { }

        internal MP3SharpException(string message, Exception inner) : base(message, inner) { }
    }
}
