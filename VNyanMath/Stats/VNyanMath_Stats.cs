using System;
using System.Collections.Generic;
using UnityEngine;
using VNyanInterface;

namespace VNyanMath_Stats
{
    public class VNyanMath_Stats : MonoBehaviour
    {
        // Set private variables
        private int VNyanMath_Stats_nSamples = 30;
        private string VNyanMath_Stats_parameterToRead = "VNyanMath_Stats_Input";

        private float VNyanMath_Stats_parameterInput = 0.0f;

        // List of floats to track the last 60 frames of data
        private float[] VNyanMath_Stats_lastNSamples = new float[0];
        private int VNyanMath_Stats_sampleIndex = 0;  // We will keep the array a constant size and instead replace items in it cyclically
        private float VNyanMath_Stats_sumSamples = 0.0f;

        // Variables for final output
        private float VNyanMath_Stats_meanSamples = 0.0f;
        private float VNyanMath_Stats_STD = 0.0f;
        private float VNyanMath_Stats_Variance = 0.0f;
        private string VNyanMath_Stats_VarianceName = "VNyanMath_Var";
        private string VNyanMath_Stats_MeanName = "VNyanMath_Mean";
        private string VNyanMath_Stats_STDName = "VNyanMath_STD";


        // Initial run of script
        void Start()
        {
            // Re-initialize our container with our sample size
            VNyanMath_Stats_lastNSamples = new float[VNyanMath_Stats_nSamples];

            // Set parameter in VNyan
            VNyanInterface.VNyanInterface.VNyanParameter.setVNyanParameterFloat(VNyanMath_Stats_VarianceName, VNyanMath_Stats_Variance);
        }

        // Update is called once per frame
        void Update()
        {
            // Get current input from parameterToRead
            VNyanMath_Stats_parameterInput = (float)VNyanInterface.VNyanInterface.VNyanParameter.getVNyanParameterFloat(VNyanMath_Stats_parameterToRead);
            // Get current frame index (mod 60 has it loop)
            VNyanMath_Stats_sampleIndex = (VNyanMath_Stats_sampleIndex + 1) % VNyanMath_Stats_nSamples;
            // insert into the array to keep our samples across frames.
            VNyanMath_Stats_lastNSamples[VNyanMath_Stats_sampleIndex] = VNyanMath_Stats_parameterInput;

            // Calculate sum of samples
            VNyanMath_Stats_sumSamples = 0;
            for (int i = 0; i < VNyanMath_Stats_nSamples; i++)
            {
                VNyanMath_Stats_sumSamples += VNyanMath_Stats_lastNSamples[i];
            }

            // Calculate Mean of samples
            VNyanMath_Stats_meanSamples = VNyanMath_Stats_sumSamples / VNyanMath_Stats_nSamples;

            // Calculate Variance of samples
            VNyanMath_Stats_Variance = 0;
            for (int i = 0; i < VNyanMath_Stats_nSamples; i++)
            {
                VNyanMath_Stats_Variance = VNyanMath_Stats_Variance + (VNyanMath_Stats_lastNSamples[i] - VNyanMath_Stats_meanSamples) * (VNyanMath_Stats_lastNSamples[i] - VNyanMath_Stats_meanSamples) / (VNyanMath_Stats_nSamples - 1);
            }
            VNyanMath_Stats_STD = (float)Math.Sqrt(VNyanMath_Stats_Variance);

            // Output into parameter
            if (VNyanMath_Stats_sampleIndex == 0)
            {
                VNyanInterface.VNyanInterface.VNyanParameter.setVNyanParameterFloat(VNyanMath_Stats_VarianceName, (float)VNyanMath_Stats_Variance);
                VNyanInterface.VNyanInterface.VNyanParameter.setVNyanParameterFloat(VNyanMath_Stats_MeanName, (float)VNyanMath_Stats_meanSamples);
                VNyanInterface.VNyanInterface.VNyanParameter.setVNyanParameterFloat(VNyanMath_Stats_STDName, (float)VNyanMath_Stats_STD);
            }
        }
    }
}
