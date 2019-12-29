using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private static readonly ICollection<string> UnknownStringProperties = new List<string>
        {
            "email",
            "user_id",
            "picture",
            "first_name",
            "last_name"
        };

        private static readonly ICollection<string> UnknownObjectProperties = new List<string>
        {
            "identities",
            "last_infos",
            "blocked_for"
        };

        [TestCase("Files\\testfile1.json")]
        [TestCase("Files\\testfile2.json")]
        public async Task Should_Get_Valid_Dynamic_Properties_With_UserProfile_V1(string filename)
        {
            // arrange
            var json = await File.ReadAllTextAsync(filename);

            // act
            var userProfile = JsonConvert.DeserializeObject<Lib.Models.V1.UserProfile>(json);

            // assert
            userProfile.Should().NotBeNull();
            userProfile.FirstName.Should().NotBeNullOrWhiteSpace();
            userProfile.LastName.Should().NotBeNullOrWhiteSpace();

            foreach (var propertyName in UnknownStringProperties)
            {
                userProfile[propertyName].Should().NotBeNull();
                userProfile.GetPropertyValue<string>(propertyName).Should().NotBeNullOrEmpty();
            }

            foreach (var propertyName in UnknownObjectProperties)
            {
                userProfile[propertyName].Should().NotBeNull();
                userProfile.GetPropertyValue<object>(propertyName).Should().NotBeNull();
            }
        }

        [TestCase("Files\\testfile1.json")]
        [TestCase("Files\\testfile2.json")]
        public async Task Should_Get_Valid_Dynamic_Properties_With_UserProfile_V2(string filename)
        {
            // arrange
            var json = await File.ReadAllTextAsync(filename);

            // act
            var userProfile = JsonConvert.DeserializeObject<Lib.Models.V2.UserProfile>(json);

            // assert
            userProfile.Should().NotBeNull();
            userProfile.FirstName.Should().NotBeNullOrEmpty();
            userProfile.LastName.Should().NotBeNullOrEmpty();

            foreach (var propertyName in UnknownStringProperties)
            {
                userProfile[propertyName].Should().NotBeNull();
                userProfile.GetPropertyValue<string>(propertyName).Should().NotBeNullOrEmpty();
            }

            foreach (var propertyName in UnknownObjectProperties)
            {
                userProfile[propertyName].Should().NotBeNull();
                userProfile.GetPropertyValue<object>(propertyName).Should().NotBeNull();
            }
        }

        [TestCase("Files\\testfile1.json")]
        [TestCase("Files\\testfile2.json")]
        public async Task Should_Get_Valid_Dynamic_Properties_With_UserProfile_V3(string filename)
        {
            // arrange
            var json = await File.ReadAllTextAsync(filename);

            // act
            var userProfile = JsonConvert.DeserializeObject<Lib.Models.V3.UserProfile>(json);

            // assert
            userProfile.Should().NotBeNull();
            userProfile.FirstName.Should().NotBeNullOrEmpty();
            userProfile.LastName.Should().NotBeNullOrEmpty();

            foreach (var propertyName in UnknownStringProperties)
            {
                userProfile[propertyName].Should().NotBeNull();
                userProfile.GetPropertyValue<string>(propertyName).Should().NotBeNullOrEmpty();
            }
            
            foreach (var propertyName in UnknownObjectProperties)
            {
                userProfile[propertyName].Should().NotBeNull();
                userProfile.GetPropertyValue<object>(propertyName).Should().NotBeNull();
            }
        }

        [TestCase("Files\\testfile1.json", typeof(Lib.Models.V1.UserProfile))]
        [TestCase("Files\\testfile2.json", typeof(Lib.Models.V1.UserProfile))]
        [TestCase("Files\\testfile1.json", typeof(Lib.Models.V2.UserProfile))]
        [TestCase("Files\\testfile2.json", typeof(Lib.Models.V2.UserProfile))]
        [TestCase("Files\\testfile1.json", typeof(Lib.Models.V3.UserProfile))]
        [TestCase("Files\\testfile2.json", typeof(Lib.Models.V3.UserProfile))]
        public async Task Should_Have_Equivalents(string filename, Type userProfileType)
        {
            // arrange
            var jsonBefore = CleanUp(await File.ReadAllTextAsync(filename));

            // act
            var userProfile = JsonConvert.DeserializeObject(jsonBefore, userProfileType);
            var jsonAfter = CleanUp(JsonConvert.SerializeObject(userProfile));

            // assert
            userProfile.Should().NotBeNull();
            jsonAfter.Should().NotBeNullOrWhiteSpace();
            jsonAfter.Length.Should().BeGreaterOrEqualTo(jsonBefore.Length);
        }

        private static string CleanUp(string content)
        {
            return string.IsNullOrEmpty(content) ? content : JObject.Parse(content).ToString(Formatting.None);
        }
    }
}