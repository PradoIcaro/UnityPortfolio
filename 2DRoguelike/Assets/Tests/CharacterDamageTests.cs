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
        Player m_player = new Player();

        [Test]
        public void CharacterDamageTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            m_player.GetDamageDealt();


        }
    }
}
