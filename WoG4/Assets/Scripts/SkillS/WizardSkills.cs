using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkills : MonoBehaviour
{
    public int getSkillMP(int skillID)
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

        return 0;
    }



    public int getMPFireball(int skillLevel)
    {
        return 30;
    }

    public int getMPGhostEye(int skillLevel)
    {
        return 25;
    }

    public int getMPICeRock(int skillLevel)
    {
        return 30;
    }

    public int getMPLightningBolt(int skillLevel)
    {
        return 35;
    }

    public int getMPShadowMove(int skillLevel)
    {
        return 40;
    }

    public int getMPDarkFlame(int skillLevel)
    {
        return 45;
    }

    public int getMPSlurm(int skillLevel)
    {
        return 50;
    }

    public int getMPWolfcry(int skillLevel)
    {
        return 50;
    }

    public int getMPMeteor(int skillLevel)
    {
        return 55;
    }

    public int getMPPowerBlast(int skillLevel)
    {
        return 60;
    }

    public int getMPInferno(int skillLevel)
    {
        return 65;
    }


}
