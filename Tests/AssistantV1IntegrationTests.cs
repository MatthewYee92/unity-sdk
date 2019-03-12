/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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

using System;
using System.Collections;
using System.Collections.Generic;
using IBM.Cloud.SDK;
using IBM.Watson.Assistant.V1;
using IBM.Watson.Assistant.V1.Model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace IBM.Watson.Tests
{
    public class AssistantV1IntegrationTests
    {
        private AssistantService service;
        private string versionDate = "2019-02-13";
        private Dictionary<string, object> customData;
        private Dictionary<string, string> customHeaders = new Dictionary<string, string>();
        private string workspaceId;
        private string createdWorkspaceName = "unity-sdk-example-workspace-delete";
        private string createdWorkspaceDescription = "A Workspace created by the Unity SDK Assistant example script. Please delete this.";
        private string createdWorkspaceLanguage = "en";
        private string updatedWorkspaceName = "unity-sdk-example-workspace-delete-updated";
        private string updatedWorkspaceDescription = "A Workspace created by the Unity SDK Assistant example script. Please delete this. (updated)";
        private string createdIntentName = "weather";
        private string createdIntentDescription = "An intent created from the Unity SDK - Please delete this.";
        private string updatedIntentName = "conditions";
        private string updatedIntentDescription = "An intent created from the Unity SDK - Please delete this. (updated)";
        private string createdExampleText = "How hot is it today";
        private string updatedExampleText = "Is it raining outside";
        private string createdCounterExampleText = "Is it raining outside";
        private string updatedCounterExampleText = "How hot is it today";
        private string createdEntityName = "Austin";
        private string createdEntityDescription = "An entity created from the Unity SDK - Please delete this";
        private string updatedEntityName = "Texas";
        private string updatedEntityDescription = "An entity created from the Unity SDK - Please delete this (updated)";
        private string createdValueText = "IBM";
        private string updatedValueText = "Watson";
        private string createdSynonymText = "Hello";
        private string updatedSynonymText = "Hi";
        private string createdDialogNode = "dialogNode";
        private string createdDialogNodeDescription = "A dialog node created from the Unity SDK - Please delete this";
        private string updatedDialogNode = "dialogNodeUpdated";
        private string updatedDialogNodeDescription = "A dialog node created from the Unity SDK - Please delete this (updated)";

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            LogSystem.InstallDefaultReactors();
            customHeaders.Add("X-Watson-Test", "1");
        }

        [UnitySetUp]
        public IEnumerator UnityTestSetup()
        {
            if (service == null)
            {
                service = new AssistantService(versionDate);
            }

            while (!service.Credentials.HasIamTokenData())
                yield return null;
        }

        [SetUp]
        public void TestSetup()
        {
            customData = new Dictionary<string, object>();
            customData.Add(Constants.String.CUSTOM_REQUEST_HEADERS, customHeaders);
        }

        [UnityTest, Order(0)]
        public IEnumerator TestMessage()
        {
            workspaceId = Environment.GetEnvironmentVariable("CONVERSATION_WORKSPACE_ID");
            JToken context = null;
            MessageResponse messageResponse = null;
            JToken conversationId = null;
            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    messageResponse = response.Result;
                    context = messageResponse.Context;
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", messageResponse.Output["generic"][0]["text"]);
                    (context as JObject).TryGetValue("conversation_id", out conversationId);
                    Assert.IsNotNull(context);
                    Assert.IsNotNull(conversationId);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                nodesVisitedDetails: true,
                customData: customData
            );

            while (messageResponse == null)
                yield return null;

            customData = new Dictionary<string, object>();
            customData.Add(Constants.String.CUSTOM_REQUEST_HEADERS, customHeaders);

            messageResponse = null;
            JObject input = new JObject();
            JToken conversationId1 = null;
            input.Add("text", "Are you open on Christmas?");
            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...Are you open on Christmas?");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    messageResponse = response.Result;
                    context = messageResponse.Context;
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", messageResponse.Output["generic"][0]["text"]);
                    (context as JObject).TryGetValue("conversation_id", out conversationId1);

                    Assert.IsNotNull(context);
                    Assert.IsNotNull(conversationId1);
                    Assert.IsTrue(conversationId1.ToString() == conversationId.ToString());
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                input: input,
                context: context as JObject,
                nodesVisitedDetails: true,
                customData: customData
            );

            while (messageResponse == null)
                yield return null;

            customData = new Dictionary<string, object>();
            customData.Add(Constants.String.CUSTOM_REQUEST_HEADERS, customHeaders);

            messageResponse = null;
            input = new JObject();
            JToken conversationId2 = null;
            input.Add("text", "What are your hours?");
            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...What are your hours?");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    messageResponse = response.Result;
                    context = messageResponse.Context;
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", messageResponse.Output["generic"][0]["text"]);
                    (context as JObject).TryGetValue("conversation_id", out conversationId2);

                    Assert.IsNotNull(context);
                    Assert.IsNotNull(conversationId2);
                    Assert.IsTrue(conversationId2.ToString() == conversationId.ToString());
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                input: input,
                context: context as JObject,
                nodesVisitedDetails: true,
                customData: customData
            );

            while (messageResponse == null)
                yield return null;

            customData = new Dictionary<string, object>();
            customData.Add(Constants.String.CUSTOM_REQUEST_HEADERS, customHeaders);

            messageResponse = null;
            input = new JObject();
            JToken conversationId3 = null;
            input.Add("text", "I'd like to make an appointment for 12pm.");
            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...I'd like to make an appointment for 12pm.");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    messageResponse = response.Result;
                    context = messageResponse.Context;
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", messageResponse.Output["generic"][0]["text"]);
                    (context as JObject).TryGetValue("conversation_id", out conversationId3);

                    Assert.IsNotNull(context);
                    Assert.IsNotNull(conversationId3);
                    Assert.IsTrue(conversationId3.ToString() == conversationId.ToString());
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                input: input,
                context: context as JObject,
                nodesVisitedDetails: true,
                customData: customData
            );

            while (messageResponse == null)
                yield return null;

            customData = new Dictionary<string, object>();
            customData.Add(Constants.String.CUSTOM_REQUEST_HEADERS, customHeaders);

            messageResponse = null;
            input = new JObject();
            JToken conversationId4 = null;
            input.Add("text", "On Friday please.");
            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...On Friday please.");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    messageResponse = response.Result;
                    context = messageResponse.Context;
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", messageResponse.Output["generic"][0]["text"]);
                    (context as JObject).TryGetValue("conversation_id", out conversationId4);

                    Assert.IsNotNull(context);
                    Assert.IsNotNull(conversationId4);
                    Assert.IsTrue(conversationId4.ToString() == conversationId.ToString());
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                input: input,
                context: context as JObject,
                nodesVisitedDetails: true,
                customData: customData
            );

            while (messageResponse == null)
                yield return null;

            customData = new Dictionary<string, object>();
            customData.Add(Constants.String.CUSTOM_REQUEST_HEADERS, customHeaders);

            messageResponse = null;
            input = new JObject();
            JToken conversationId5 = null;
            input.Add("text", "Yes.");
            Log.Debug("AssistantV1IntegrationTests", "Attempting to Message...Yes.");
            service.Message(
                callback: (DetailedResponse<MessageResponse> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    messageResponse = response.Result;
                    context = messageResponse.Context;
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", messageResponse.Output["generic"][0]["text"]);
                    (context as JObject).TryGetValue("conversation_id", out conversationId5);

                    Assert.IsNotNull(context);
                    Assert.IsNotNull(conversationId5);
                    Assert.IsTrue(conversationId5.ToString() == conversationId.ToString());
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                input: input,
                context: context as JObject,
                nodesVisitedDetails: true,
                customData: customData
            );

            while (messageResponse == null)
                yield return null;
        }

        [UnityTest, Order(1)]
        public IEnumerator TestCreateWorkspace()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateWorkspace...");
            Workspace createWorkspaceResponse = null;
            service.CreateWorkspace(
                callback: (DetailedResponse<Workspace> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateWorkspace result: {0}", customResponseData["json"].ToString());
                    createWorkspaceResponse = response.Result;
                    workspaceId = createWorkspaceResponse.WorkspaceId;
                    Assert.IsNotNull(createWorkspaceResponse);
                    Assert.IsNotNull(workspaceId);
                    Assert.IsTrue(createWorkspaceResponse.Name == createdWorkspaceName);
                    Assert.IsTrue(createWorkspaceResponse.Description == createdWorkspaceDescription);
                    Assert.IsTrue(createWorkspaceResponse.Language == createdWorkspaceLanguage);
                    Assert.IsNull(error);
                },
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage,
                learningOptOut: true,
                customData: customData
            );

            while (createWorkspaceResponse == null)
                yield return null;
        }

        [UnityTest, Order(2)]
        public IEnumerator TestGetWorkspace()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetWorkspace...");
            Workspace getWorkspaceResponse = null;
            service.GetWorkspace(
                callback: (DetailedResponse<Workspace> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetWorkspace result: {0}", customResponseData["json"].ToString());
                    getWorkspaceResponse = response.Result;
                    Assert.IsNotNull(getWorkspaceResponse);
                    Assert.IsTrue(getWorkspaceResponse.WorkspaceId == workspaceId);
                    Assert.IsTrue(getWorkspaceResponse.Name == createdWorkspaceName);
                    Assert.IsTrue(getWorkspaceResponse.Description == createdWorkspaceDescription);
                    Assert.IsTrue(getWorkspaceResponse.Language == createdWorkspaceLanguage);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                export: true,
                includeAudit: true,
                sort: "-name",
                customData: customData
            );

            while (getWorkspaceResponse == null)
                yield return null;
        }

        [UnityTest, Order(3)]
        public IEnumerator TestListWorkspaces()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListWorkspaces...");
            WorkspaceCollection listWorkspacesResponse = null;
            service.ListWorkspaces(
                callback: (DetailedResponse<WorkspaceCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListWorkspaces result: {0}", customResponseData["json"].ToString());
                    listWorkspacesResponse = response.Result;
                    Assert.IsNotNull(listWorkspacesResponse);
                    Assert.IsNotNull(listWorkspacesResponse.Workspaces);
                    Assert.IsTrue(listWorkspacesResponse.Workspaces.Count > 0);
                    Assert.IsNull(error);
                },
                pageLimit: 1,
                includeCount: true,
                sort: "-name",
                includeAudit: true,
                customData: customData
            );

            while (listWorkspacesResponse == null)
                yield return null;
        }

        [UnityTest, Order(4)]
        public IEnumerator TestUpdateWorkspace()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateWorkspace...");
            Workspace updateWorkspaceResponse = null;
            service.UpdateWorkspace(
                callback: (DetailedResponse<Workspace> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateWorkspace result: {0}", customResponseData["json"].ToString());
                    updateWorkspaceResponse = response.Result;
                    Assert.IsNotNull(updateWorkspaceResponse);
                    Assert.IsTrue(updateWorkspaceResponse.Name == updatedWorkspaceName);
                    Assert.IsTrue(updateWorkspaceResponse.Description == updatedWorkspaceDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                name: updatedWorkspaceName,
                description: updatedWorkspaceDescription,
                language: createdWorkspaceLanguage,
                learningOptOut: true,
                append: false,
                customData: customData
            );

            while (updateWorkspaceResponse == null)
                yield return null;
        }

        [UnityTest, Order(5)]
        public IEnumerator TestCreateIntent()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateIntent...");
            Intent createIntentResponse = null;
            service.CreateIntent(
                callback: (DetailedResponse<Intent> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateIntent result: {0}", customResponseData["json"].ToString());
                    createIntentResponse = response.Result;
                    Assert.IsNotNull(createIntentResponse);
                    Assert.IsTrue(createIntentResponse._Intent == createdIntentName);
                    Assert.IsTrue(createIntentResponse.Description == createdIntentDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: createdIntentName,
                description: createdIntentDescription,
                customData: customData
            );

            while (createIntentResponse == null)
                yield return null;
        }

        [UnityTest, Order(6)]
        public IEnumerator TestGetIntent()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetIntent...");
            Intent getIntentResponse = null;
            service.GetIntent(
                callback: (DetailedResponse<Intent> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetIntent result: {0}", customResponseData["json"].ToString());
                    getIntentResponse = response.Result;
                    Assert.IsNotNull(getIntentResponse);
                    Assert.IsTrue(getIntentResponse._Intent == createdIntentName);
                    Assert.IsTrue(getIntentResponse.Description == createdIntentDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: createdIntentName,
                export: true,
                includeAudit: true,
                customData: customData
            );

            while (getIntentResponse == null)
                yield return null;
        }

        [UnityTest, Order(7)]
        public IEnumerator TestListIntents()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListIntents...");
            IntentCollection listIntentsResponse = null;
            service.ListIntents(
                callback: (DetailedResponse<IntentCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListIntents result: {0}", customResponseData["json"].ToString());
                    listIntentsResponse = response.Result;
                    Assert.IsNotNull(listIntentsResponse);
                    Assert.IsNotNull(listIntentsResponse.Intents);
                    Assert.IsTrue(listIntentsResponse.Intents.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                export: true,
                pageLimit: 1,
                includeCount: true,
                sort: "-name",
                includeAudit: true,
                customData: customData
            );

            while (listIntentsResponse == null)
                yield return null;
        }

        [UnityTest, Order(8)]
        public IEnumerator TestUpdateIntent()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateIntent...");
            Intent updateIntentResponse = null;
            service.UpdateIntent(
                callback: (DetailedResponse<Intent> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateIntent result: {0}", customResponseData["json"].ToString());
                    updateIntentResponse = response.Result;
                    Assert.IsNotNull(updateIntentResponse);
                    Assert.IsTrue(updateIntentResponse._Intent == updatedIntentName);
                    Assert.IsTrue(updateIntentResponse.Description == updatedIntentDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: createdIntentName,
                newIntent: updatedIntentName,
                newDescription: updatedIntentDescription,
                customData: customData
            );

            while (updateIntentResponse == null)
                yield return null;
        }

        [UnityTest, Order(9)]
        public IEnumerator TestCreateExample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateExample...");
            Example createExampleResponse = null;
            service.CreateExample(
                callback: (DetailedResponse<Example> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateExample result: {0}", customResponseData["json"].ToString());
                    createExampleResponse = response.Result;
                    Assert.IsNotNull(createExampleResponse);
                    Assert.IsTrue(createExampleResponse.Text == createdExampleText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: updatedIntentName,
                text: createdExampleText,
                customData: customData
            );

            while (createExampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(10)]
        public IEnumerator TestGetExample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetExample...");
            Example getExampleResponse = null;
            service.GetExample(
                callback: (DetailedResponse<Example> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetExample result: {0}", customResponseData["json"].ToString());
                    getExampleResponse = response.Result;
                    Assert.IsNotNull(getExampleResponse);
                    Assert.IsTrue(getExampleResponse.Text == createdExampleText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: updatedIntentName,
                text: createdExampleText,
                includeAudit: true,
                customData: customData
            );

            while (getExampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(11)]
        public IEnumerator TestListExamples()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListExamples...");
            ExampleCollection listExamplesResponse = null;
            service.ListExamples(
                callback: (DetailedResponse<ExampleCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListExamples result: {0}", customResponseData["json"].ToString());
                    listExamplesResponse = response.Result;
                    Assert.IsNotNull(listExamplesResponse);
                    Assert.IsNotNull(listExamplesResponse.Examples);
                    Assert.IsTrue(listExamplesResponse.Examples.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: updatedIntentName,
                pageLimit: 1,
                includeCount: true,
                sort: "-text",
                includeAudit: true,
                customData: customData
            );

            while (listExamplesResponse == null)
                yield return null;
        }

        [UnityTest, Order(12)]
        public IEnumerator TestUpdateExample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateExample...");
            Example updateExampleResponse = null;
            service.UpdateExample(
                callback: (DetailedResponse<Example> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateExample result: {0}", customResponseData["json"].ToString());
                    updateExampleResponse = response.Result;
                    Assert.IsNotNull(updateExampleResponse);
                    Assert.IsTrue(updateExampleResponse.Text == updatedExampleText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: updatedIntentName,
                text: createdExampleText,
                newText: updatedExampleText,
                customData: customData
            );

            while (updateExampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(13)]
        public IEnumerator TestCreateCounterexample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateCounterexample...");
            Counterexample createCounterexampleResponse = null;
            service.CreateCounterexample(
                callback: (DetailedResponse<Counterexample> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateCounterexample result: {0}", customResponseData["json"].ToString());
                    createCounterexampleResponse = response.Result;
                    Assert.IsNotNull(createCounterexampleResponse);
                    Assert.IsTrue(createCounterexampleResponse.Text == createdCounterExampleText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                text: createdCounterExampleText,
                customData: customData
            );

            while (createCounterexampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(14)]
        public IEnumerator TestGetCounterexample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetCounterexample...");
            Counterexample getCounterexampleResponse = null;
            service.GetCounterexample(
                callback: (DetailedResponse<Counterexample> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetCounterexample result: {0}", customResponseData["json"].ToString());
                    getCounterexampleResponse = response.Result;
                    Assert.IsNotNull(getCounterexampleResponse);
                    Assert.IsTrue(getCounterexampleResponse.Text == createdCounterExampleText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                text: createdCounterExampleText,
                includeAudit: true,
                customData: customData
            );

            while (getCounterexampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(15)]
        public IEnumerator TestListCounterexamples()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListCounterexamples...");
            CounterexampleCollection listCounterexamplesResponse = null;
            service.ListCounterexamples(
                callback: (DetailedResponse<CounterexampleCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListCounterexamples result: {0}", customResponseData["json"].ToString());
                    listCounterexamplesResponse = response.Result;
                    Assert.IsNotNull(listCounterexamplesResponse);
                    Assert.IsNotNull(listCounterexamplesResponse.Counterexamples);
                    Assert.IsTrue(listCounterexamplesResponse.Counterexamples.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                pageLimit: 1,
                includeCount: true,
                sort: "-text",
                includeAudit: true,
                customData: customData
            );

            while (listCounterexamplesResponse == null)
                yield return null;
        }

        [UnityTest, Order(16)]
        public IEnumerator TestUpdateCounterexample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateCounterexample...");
            Counterexample updateCounterexampleResponse = null;
            service.UpdateCounterexample(
                callback: (DetailedResponse<Counterexample> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateCounterexample result: {0}", customResponseData["json"].ToString());
                    updateCounterexampleResponse = response.Result;
                    Assert.IsNotNull(updateCounterexampleResponse);
                    Assert.IsTrue(updateCounterexampleResponse.Text == updatedCounterExampleText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                text: createdCounterExampleText,
                newText: updatedCounterExampleText,
                customData: customData
            );

            while (updateCounterexampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(17)]
        public IEnumerator TestCreateEntity()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateEntity...");
            Entity createEntityResponse = null;
            service.CreateEntity(
                callback: (DetailedResponse<Entity> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateEntity result: {0}", customResponseData["json"].ToString());
                    createEntityResponse = response.Result;
                    Assert.IsNotNull(createEntityResponse);
                    Assert.IsTrue(createEntityResponse._Entity == createdEntityName);
                    Assert.IsTrue(createEntityResponse.Description == createdEntityDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: createdEntityName,
                description: createdEntityDescription,
                fuzzyMatch: true,
                customData: customData
            );

            while (createEntityResponse == null)
                yield return null;
        }

        [UnityTest, Order(18)]
        public IEnumerator TestGetEntity()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetEntity...");
            Entity getEntityResponse = null;
            service.GetEntity(
                callback: (DetailedResponse<Entity> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetEntity result: {0}", customResponseData["json"].ToString());
                    getEntityResponse = response.Result;
                    Assert.IsNotNull(getEntityResponse);
                    Assert.IsTrue(getEntityResponse._Entity == createdEntityName);
                    Assert.IsTrue(getEntityResponse.Description == createdEntityDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: createdEntityName,
                export: true,
                includeAudit: true,
                customData: customData
            );

            while (getEntityResponse == null)
                yield return null;
        }

        [UnityTest, Order(19)]
        public IEnumerator TestListEntities()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListEntities...");
            EntityCollection listEntitiesResponse = null;
            service.ListEntities(
                callback: (DetailedResponse<EntityCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListEntities result: {0}", customResponseData["json"].ToString());
                    listEntitiesResponse = response.Result;
                    Assert.IsNotNull(listEntitiesResponse);
                    Assert.IsNotNull(listEntitiesResponse.Entities);
                    Assert.IsTrue(listEntitiesResponse.Entities.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                export: true,
                pageLimit: 1,
                includeCount: true,
                sort: "-entity",
                includeAudit: true,
                customData: customData
            );

            while (listEntitiesResponse == null)
                yield return null;
        }

        [UnityTest, Order(20)]
        public IEnumerator TestUpdateEntity()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateEntity...");
            Entity updateEntityResponse = null;
            service.UpdateEntity(
                callback: (DetailedResponse<Entity> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateEntity result: {0}", customResponseData["json"].ToString());
                    updateEntityResponse = response.Result;
                    Assert.IsNotNull(updateEntityResponse);
                    Assert.IsTrue(updateEntityResponse._Entity == updatedEntityName);
                    Assert.IsTrue(updateEntityResponse.Description == updatedEntityDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: createdEntityName,
                newEntity: updatedEntityName,
                newDescription: updatedEntityDescription,
                newFuzzyMatch: true,
                customData: customData
            );

            while (updateEntityResponse == null)
                yield return null;
        }

        [UnityTest, Order(21)]
        public IEnumerator TestListMentions()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListMentions...");
            EntityMentionCollection listMentionsResponse = null;
            service.ListMentions(
                callback: (DetailedResponse<EntityMentionCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListMentions result: {0}", customResponseData["json"].ToString());
                    listMentionsResponse = response.Result;
                    Assert.IsNotNull(listMentionsResponse);
                    Assert.IsNotNull(listMentionsResponse.Examples);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                export: true,
                includeAudit: true,
                customData: customData
            );

            while (listMentionsResponse == null)
                yield return null;
        }

        [UnityTest, Order(22)]
        public IEnumerator TestCreateValue()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateValue...");
            Value createValueResponse = null;
            service.CreateValue(
                callback: (DetailedResponse<Value> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateValue result: {0}", customResponseData["json"].ToString());
                    createValueResponse = response.Result;
                    Assert.IsNotNull(createValueResponse);
                    Assert.IsTrue(createValueResponse._Value == createdValueText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: createdValueText,
                customData: customData
            );

            while (createValueResponse == null)
                yield return null;
        }

        [UnityTest, Order(23)]
        public IEnumerator TestGetValue()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetValue...");
            Value getValueResponse = null;
            service.GetValue(
                callback: (DetailedResponse<Value> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetValue result: {0}", customResponseData["json"].ToString());
                    getValueResponse = response.Result;
                    Assert.IsNotNull(getValueResponse);
                    Assert.IsTrue(getValueResponse._Value == createdValueText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: createdValueText,
                export: true,
                includeAudit: true,
                customData: customData
            );

            while (getValueResponse == null)
                yield return null;
        }

        [UnityTest, Order(24)]
        public IEnumerator TestListValues()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListValues...");
            ValueCollection listValuesResponse = null;
            service.ListValues(
                callback: (DetailedResponse<ValueCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListValues result: {0}", customResponseData["json"].ToString());
                    listValuesResponse = response.Result;
                    Assert.IsNotNull(listValuesResponse);
                    Assert.IsNotNull(listValuesResponse.Values);
                    Assert.IsTrue(listValuesResponse.Values.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                export: true,
                pageLimit: 1,
                includeCount: true,
                sort: "-value",
                includeAudit: true,
                customData: customData
            );

            while (listValuesResponse == null)
                yield return null;
        }

        [UnityTest, Order(25)]
        public IEnumerator TestUpdateValue()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateValue...");
            Value updateValueResponse = null;
            service.UpdateValue(
                callback: (DetailedResponse<Value> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateValue result: {0}", customResponseData["json"].ToString());
                    updateValueResponse = response.Result;
                    Assert.IsNotNull(updateValueResponse);
                    Assert.IsTrue(updateValueResponse._Value == updatedValueText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: createdValueText,
                newValue: updatedValueText,
                customData: customData
            );

            while (updateValueResponse == null)
                yield return null;
        }

        [UnityTest, Order(26)]
        public IEnumerator TestCreateSynonym()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateSynonym...");
            Synonym createSynonymResponse = null;
            service.CreateSynonym(
                callback: (DetailedResponse<Synonym> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateSynonym result: {0}", customResponseData["json"].ToString());
                    createSynonymResponse = response.Result;
                    Assert.IsNotNull(createSynonymResponse);
                    Assert.IsTrue(createSynonymResponse._Synonym == createdSynonymText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: updatedValueText,
                synonym: createdSynonymText,
                customData: customData

            );

            while (createSynonymResponse == null)
                yield return null;
        }

        [UnityTest, Order(27)]
        public IEnumerator TestGetSynonym()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetSynonym...");
            Synonym getSynonymResponse = null;
            service.GetSynonym(
                callback: (DetailedResponse<Synonym> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetSynonym result: {0}", customResponseData["json"].ToString());
                    getSynonymResponse = response.Result;
                    Assert.IsNotNull(getSynonymResponse);
                    Assert.IsTrue(getSynonymResponse._Synonym == createdSynonymText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: updatedValueText,
                synonym: createdSynonymText,
                includeAudit: true,
                customData: customData
            );

            while (getSynonymResponse == null)
                yield return null;
        }

        [UnityTest, Order(28)]
        public IEnumerator TestListSynonyms()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListSynonyms...");
            SynonymCollection listSynonymsResponse = null;
            service.ListSynonyms(
                callback: (DetailedResponse<SynonymCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListSynonyms result: {0}", customResponseData["json"].ToString());
                    listSynonymsResponse = response.Result;
                    Assert.IsNotNull(listSynonymsResponse);
                    Assert.IsNotNull(listSynonymsResponse.Synonyms);
                    Assert.IsTrue(listSynonymsResponse.Synonyms.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: updatedValueText,
                pageLimit: 1,
                includeCount: true,
                sort: "-synonym",
                includeAudit: true,
                customData: customData
            );

            while (listSynonymsResponse == null)
                yield return null;
        }

        [UnityTest, Order(29)]
        public IEnumerator TestUpdateSynonym()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateSynonym...");
            Synonym updateSynonymResponse = null;
            service.UpdateSynonym(
                callback: (DetailedResponse<Synonym> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateSynonym result: {0}", customResponseData["json"].ToString());
                    updateSynonymResponse = response.Result;
                    Assert.IsNotNull(updateSynonymResponse);
                    Assert.IsTrue(updateSynonymResponse._Synonym == updatedSynonymText);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: updatedValueText,
                synonym: createdSynonymText,
                newSynonym: updatedSynonymText,
                customData: customData
            );

            while (updateSynonymResponse == null)
                yield return null;
        }

        [UnityTest, Order(30)]
        public IEnumerator TestCreateDialogNode()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to CreateDialogNode...");
            DialogNode createDialogNodeResponse = null;
            service.CreateDialogNode(
                callback: (DetailedResponse<DialogNode> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "CreateDialogNode result: {0}", customResponseData["json"].ToString());
                    createDialogNodeResponse = response.Result;
                    Assert.IsNotNull(createDialogNodeResponse);
                    Assert.IsTrue(createDialogNodeResponse._DialogNode == createdDialogNode);
                    Assert.IsTrue(createDialogNodeResponse.Description == createdDialogNodeDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                dialogNode: createdDialogNode,
                description: createdDialogNodeDescription,
                customData: customData
            );

            while (createDialogNodeResponse == null)
                yield return null;
        }

        [UnityTest, Order(31)]
        public IEnumerator TestGetDialogNode()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to GetDialogNode...");
            DialogNode getDialogNodeResponse = null;
            service.GetDialogNode(
                callback: (DetailedResponse<DialogNode> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "GetDialogNode result: {0}", customResponseData["json"].ToString());
                    getDialogNodeResponse = response.Result;
                    Assert.IsNotNull(getDialogNodeResponse);
                    Assert.IsTrue(getDialogNodeResponse._DialogNode == createdDialogNode);
                    Assert.IsTrue(getDialogNodeResponse.Description == createdDialogNodeDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                dialogNode: createdDialogNode,
                includeAudit: true,
                customData: customData
            );

            while (getDialogNodeResponse == null)
                yield return null;
        }

        [UnityTest, Order(32)]
        public IEnumerator TestListDialogNodes()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListDialogNodes...");
            DialogNodeCollection listDialogNodesResponse = null;
            service.ListDialogNodes(
                callback: (DetailedResponse<DialogNodeCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListDialogNodes result: {0}", customResponseData["json"].ToString());
                    listDialogNodesResponse = response.Result;
                    Assert.IsNotNull(listDialogNodesResponse);
                    Assert.IsNotNull(listDialogNodesResponse.DialogNodes);
                    Assert.IsTrue(listDialogNodesResponse.DialogNodes.Count > 0);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                pageLimit: 1,
                includeCount: true,
                sort: "-dialog_node",
                includeAudit: true,
                customData: customData
            );

            while (listDialogNodesResponse == null)
                yield return null;
        }

        [UnityTest, Order(33)]
        public IEnumerator TestUpdateDialogNode()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to UpdateDialogNode...");
            DialogNode updateDialogNodeResponse = null;
            service.UpdateDialogNode(
                callback: (DetailedResponse<DialogNode> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "UpdateDialogNode result: {0}", customResponseData["json"].ToString());
                    updateDialogNodeResponse = response.Result;
                    Assert.IsNotNull(updateDialogNodeResponse);
                    Assert.IsTrue(updateDialogNodeResponse._DialogNode == updatedDialogNode);
                    Assert.IsTrue(updateDialogNodeResponse.Description == updatedDialogNodeDescription);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                dialogNode: createdDialogNode,
                newDialogNode: updatedDialogNode,
                newDescription: updatedDialogNodeDescription,
                customData: customData
            );

            while (updateDialogNodeResponse == null)
                yield return null;
        }

        [UnityTest, Order(34)]
        public IEnumerator TestListAllLogs()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListAllLogs...");
            LogCollection listAllLogsResponse = null;
            service.ListAllLogs(
                callback: (DetailedResponse<LogCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListAllLogs result: {0}", customResponseData["json"].ToString());
                    listAllLogsResponse = response.Result;
                    Assert.IsNotNull(listAllLogsResponse);
                    Assert.IsNotNull(listAllLogsResponse.Logs);
                    Assert.IsNull(error);
                },
                filter: "(language::en,request.context.metadata.deployment::deployment_1)",
                customData: customData
            );

            while (listAllLogsResponse == null)
                yield return null;
        }

        [UnityTest, Order(35)]
        public IEnumerator TestListLogs()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to ListLogs...");
            LogCollection listLogsResponse = null;
            service.ListLogs(
                callback: (DetailedResponse<LogCollection> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "ListLogs result: {0}", customResponseData["json"].ToString());
                    listLogsResponse = response.Result;
                    Assert.IsNotNull(listLogsResponse);
                    Assert.IsNotNull(listLogsResponse.Logs);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                filter: "(language::en,request.context.metadata.deployment::deployment_1)",
                pageLimit: 1,
                customData: customData
            );

            while (listLogsResponse == null)
                yield return null;
        }

        [UnityTest, Order(91)]
        public IEnumerator TestDeleteUserData()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteUserData...");
            object deleteUserDataResponse = null;
            service.DeleteUserData(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteUserData result: {0}", customResponseData["json"].ToString());
                    deleteUserDataResponse = response.Result;
                    Assert.IsNotNull(deleteUserDataResponse);
                    Assert.IsNull(error);
                },
                customerId: "test-customer-id",
                customData: customData
            );

            while (deleteUserDataResponse == null)
                yield return null;
        }


        [UnityTest, Order(92)]
        public IEnumerator TestDeleteDialogNode()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteDialogNode...");
            object deleteDialogNodeResponse = null;
            service.DeleteDialogNode(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteDialogNode result: {0}", customResponseData["json"].ToString());
                    deleteDialogNodeResponse = response.Result;
                    Assert.IsNotNull(deleteDialogNodeResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                dialogNode: updatedDialogNode,
                customData: customData
            );

            while (deleteDialogNodeResponse == null)
                yield return null;
        }

        [UnityTest, Order(93)]
        public IEnumerator TestDeleteSynonym()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteSynonym...");
            object deleteSynonymResponse = null;
            service.DeleteSynonym(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteSynonym result: {0}", customResponseData["json"].ToString());
                    deleteSynonymResponse = response.Result;
                    Assert.IsNotNull(deleteSynonymResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: updatedValueText,
                synonym: updatedSynonymText,
                customData: customData
            );

            while (deleteSynonymResponse == null)
                yield return null;
        }

        [UnityTest, Order(94)]
        public IEnumerator TestDeleteValue()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteValue...");
            object deleteValueResponse = null;
            service.DeleteValue(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteValue result: {0}", customResponseData["json"].ToString());
                    deleteValueResponse = response.Result;
                    Assert.IsNotNull(deleteValueResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                value: updatedValueText,
                customData: customData
            );

            while (deleteValueResponse == null)
                yield return null;
        }

        [UnityTest, Order(95)]
        public IEnumerator TestDeleteEntity()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteEntity...");
            object deleteEntityResponse = null;
            service.DeleteEntity(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteEntity result: {0}", customResponseData["json"].ToString());
                    deleteEntityResponse = response.Result;
                    Assert.IsNotNull(deleteEntityResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                entity: updatedEntityName,
                customData: customData
            );

            while (deleteEntityResponse == null)
                yield return null;
        }

        [UnityTest, Order(96)]
        public IEnumerator TestDeleteCounterexample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteCounterexample...");
            object deleteCounterexampleResponse = null;
            service.DeleteCounterexample(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteCounterexample result: {0}", customResponseData["json"].ToString());
                    deleteCounterexampleResponse = response.Result;
                    Assert.IsNotNull(deleteCounterexampleResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                text: updatedCounterExampleText,
                customData: customData
            );

            while (deleteCounterexampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(97)]
        public IEnumerator TestDeleteExample()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteExample...");
            object deleteExampleResponse = null;
            service.DeleteExample(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteExample result: {0}", customResponseData["json"].ToString());
                    deleteExampleResponse = response.Result;
                    Assert.IsNotNull(deleteExampleResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: updatedIntentName,
                text: updatedExampleText,
                customData: customData
            );

            while (deleteExampleResponse == null)
                yield return null;
        }

        [UnityTest, Order(98)]
        public IEnumerator TestDeleteIntent()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteIntent...");
            object deleteIntentResponse = null;
            service.DeleteIntent(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteIntent result: {0}", customResponseData["json"].ToString());
                    deleteIntentResponse = response.Result;
                    Assert.IsNotNull(response.Result);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                intent: updatedIntentName,
                customData: customData
            );

            while (deleteIntentResponse == null)
                yield return null;
        }


        [UnityTest, Order(99)]
        public IEnumerator TestDeleteWorkspace()
        {
            Log.Debug("AssistantServiceV1IntegrationTests", "Attempting to DeleteWorkspace...");
            object deleteWorkspaceResponse = null;
            service.DeleteWorkspace(
                callback: (DetailedResponse<object> response, IBMError error, Dictionary<string, object> customResponseData) =>
                {
                    Log.Debug("AssistantServiceV1IntegrationTests", "DeleteWorkspace result: {0}", customResponseData["json"].ToString());
                    deleteWorkspaceResponse = response.Result;
                    Assert.IsNotNull(deleteWorkspaceResponse);
                    Assert.IsNull(error);
                },
                workspaceId: workspaceId,
                customData: customData
            );

            while (deleteWorkspaceResponse == null)
                yield return null;
        }
    }
}
