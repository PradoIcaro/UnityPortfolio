using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CharacterDamageTests
    {
        // A Test behaves as an ordinary method
        Player player = new Player();

        [Test]
        public void CharacterDamageTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            player.GetDamageDealt();


        }
    }
}
