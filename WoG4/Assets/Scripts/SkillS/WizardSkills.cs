using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkills : MonoBehaviour
{
    public (string color, int MP) getSkillMP(int skillID)
    {
        if (skillID == 0)
        {
            return getMPFireball(1);
        }
        if (skillID == 1)
        {
            return getMPGhostEye(1);
        }
        if (skillID == 2)
        {
            return getMPICeRock(1);
        }
        if (skillID == 3)
        {
            return getMPLightningBolt(1);
        }
        if (skillID == 4)
        {
            return getMPWolfcry(1);
        }
        if (skillID == 5)
        {
            return getMPShadowMove(1);
        }
        if (skillID == 6)
        {
            return getMPSlurm(1);
        }
        if (skillID == 7)
        {
            return getMPMeteor(1);
        }
        if (skillID == 8)
        {
            return getMPDarkFlame(1);
        }
        if (skillID == 9)
        {
            return getMPPowerBlast(1);
        }
        if (skillID == 10)
        {
            return getMPInferno(1);
        }

        return getMPInferno(1);
    }



    public (string color, int MP) getMPFireball(int skillLevel)
    {
        return ("red", 10);
    }

    public (string color, int MP) getMPGhostEye(int skillLevel)
    {
        return ("green", 6);
    }

    public (string color, int MP) getMPICeRock(int skillLevel)
    {
        return ("yellow", 7);
    }

    public (string color, int MP) getMPLightningBolt(int skillLevel)
    {
        return ("blue", 5);
    }

    public (string color, int MP) getMPShadowMove(int skillLevel)
    {
        return ("brown", 3);
    }

    public (string color, int MP) getMPDarkFlame(int skillLevel)
    {
        return ("red", 7);
    }

    public (string color, int MP) getMPSlurm(int skillLevel)
    {
        return ("red", 60);
    }

    public (string color, int MP) getMPWolfcry(int skillLevel)
    {
        return ("brown", 5);
    }

    public (string color, int MP) getMPMeteor(int skillLevel)
    {
        return ("red", 70);
    }

    public (string color, int MP) getMPPowerBlast(int skillLevel)
    {
        return ("red", 75);
    }

    public (string color, int MP) getMPInferno(int skillLevel)
    {
        return ("red", 80);
    }


}
