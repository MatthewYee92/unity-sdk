/**
* Copyright 2019 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System.Collections.Generic;
using FullSerializer;
using System;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// WorkspaceExport.
    /// </summary>
    public class WorkspaceExport
    {
        /// <summary>
        /// The current status of the workspace.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant NON_EXISTENT for Non Existent
            /// </summary>
            public const string NON_EXISTENT = "Non Existent";
            /// <summary>
            /// Constant TRAINING for Training
            /// </summary>
            public const string TRAINING = "Training";
            /// <summary>
            /// Constant FAILED for Failed
            /// </summary>
            public const string FAILED = "Failed";
            /// <summary>
            /// Constant AVAILABLE for Available
            /// </summary>
            public const string AVAILABLE = "Available";
            /// <summary>
            /// Constant UNAVAILABLE for Unavailable
            /// </summary>
            public const string UNAVAILABLE = "Unavailable";
            
        }

        /// <summary>
        /// The current status of the workspace.
        /// </summary>
        [fsProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// The name of the workspace.
        /// </summary>
        [fsProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// The description of the workspace.
        /// </summary>
        [fsProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// The language of the workspace.
        /// </summary>
        [fsProperty("language")]
        public string Language { get; set; }
        /// <summary>
        /// Any metadata that is required by the workspace.
        /// </summary>
        [fsProperty("metadata")]
        public object Metadata { get; set; }
        /// <summary>
        /// The timestamp for creation of the workspace.
        /// </summary>
        [fsProperty("created")]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// The timestamp for the last update to the workspace.
        /// </summary>
        [fsProperty("updated")]
        public virtual DateTime? Updated { get; private set; }
        /// <summary>
        /// The workspace ID of the workspace.
        /// </summary>
        [fsProperty("workspace_id")]
        public virtual string WorkspaceId { get; private set; }
        /// <summary>
        /// Whether training data from the workspace can be used by IBM for general service improvements. `true`
        /// indicates that workspace training data is not to be used.
        /// </summary>
        [fsProperty("learning_opt_out")]
        public bool? LearningOptOut { get; set; }
        /// <summary>
        /// Global settings for the workspace.
        /// </summary>
        [fsProperty("system_settings")]
        public WorkspaceSystemSettings SystemSettings { get; set; }
        /// <summary>
        /// An array of intents.
        /// </summary>
        [fsProperty("intents")]
        public List<IntentExport> Intents { get; set; }
        /// <summary>
        /// An array of entities.
        /// </summary>
        [fsProperty("entities")]
        public List<EntityExport> Entities { get; set; }
        /// <summary>
        /// An array of counterexamples.
        /// </summary>
        [fsProperty("counterexamples")]
        public List<Counterexample> Counterexamples { get; set; }
        /// <summary>
        /// An array of objects describing the dialog nodes in the workspace.
        /// </summary>
        [fsProperty("dialog_nodes")]
        public List<DialogNode> DialogNodes { get; set; }
    }


}