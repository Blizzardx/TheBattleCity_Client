using System;
using UnityEngine;

namespace Common.MicroPhone
{
    public class MicroPhoneInput
    {
        private static MicroPhoneInput m_instance;

        public float sensitivity = 100;
        public float loudness = 0;

        private static string[] micArray = null;

        const int HEADER_SIZE = 44;
        const int RECORD_TIME = 10;

        public static MicroPhoneInput getInstance()
        {
            if (m_instance == null)
            {
                micArray = Microphone.devices;
                if (micArray.Length == 0)
                {
                    Debug.LogError("Microphone.devices is null");
                }
                foreach (string deviceStr in Microphone.devices)
                {
                    Debug.Log("device name = " + deviceStr);
                }
                if (micArray.Length == 0)
                {
                    Debug.LogError("no mic device");
                }
                m_instance = new MicroPhoneInput();
            }
            return m_instance;
        }
        public void StartRecord(int recordTime, ref AudioSource audio)
        {
            if (micArray.Length == 0)
            {
                Debug.Log("No Record Device!");
                return;
            }
            if (Microphone.IsRecording(null))
            {
                Debug.Log("micro phone busy");
                return;
            }
            audio.clip = Microphone.Start("Built-in Microphone", false, recordTime, 44100); //22050         
        }
        public void StopRecord()
        {
            if (micArray.Length == 0)
            {
                Debug.Log("No Record Device!");
                return;
            }
            if (!Microphone.IsRecording(null))
            {
                return;
            }
            Microphone.End(null);

            Debug.Log("StopRecord");
        }
        public static Byte[] ConvertClipToByte(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.Log("GetClipData audio.clip is null");
                return null;
            }

            float[] samples = new float[clip.samples];

            clip.GetData(samples, 0);


            Byte[] outData = new byte[samples.Length * 2];
            //Int16[] intData = new Int16[samples.Length];
            //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

            int rescaleFactor = 32767; //to convert float to Int16

            for (int i = 0; i < samples.Length; i++)
            {
                short temshort = (short)(samples[i] * rescaleFactor);

                Byte[] temdata = System.BitConverter.GetBytes(temshort);

                outData[i * 2] = temdata[0];
                outData[i * 2 + 1] = temdata[1];


            }
            if (outData == null || outData.Length <= 0)
            {
                Debug.Log("GetClipData intData is null");
                return null;
            }
            //return intData;
            return outData;
        }
        public static Int16[] ConvertByteArrayToShort(byte[] array)
        {
            int size = array.Length / 2;
            Int16[] res = new Int16[size];
            int index = 0;
            for (int i = 0; i < size; ++i)
            {
                res[i] = System.BitConverter.ToInt16(array, index);
                index += 2;
            }

            return res;
        }
        public static AudioClip ConvertByteToClip(byte[] array)
        {
            Int16[] intArr = ConvertByteArrayToShort(array);
            if (null == intArr)
            {
                return null;
            }

            string aaastr = intArr.ToString();
            long aaalength = aaastr.Length;
            Debug.LogError("aaalength=" + aaalength);

            string aaastr1 = Convert.ToString(intArr);
            aaalength = aaastr1.Length;
            Debug.LogError("aaalength=" + aaalength);

            if (intArr.Length == 0)
            {
                Debug.Log("get intarr clipdata is null");
                return null;
            }
            //从Int16[]到float[]
            float[] samples = new float[intArr.Length];
            int rescaleFactor = 32767;
            for (int i = 0; i < intArr.Length; i++)
            {
                samples[i] = (float)intArr[i] / rescaleFactor;
            }

            //从float[]到Clip
            AudioClip clip = null;
            clip = AudioClip.Create("playRecordClip", intArr.Length, 1, 44100, false, false);
            clip.SetData(samples, 0);

            return clip;
        }
    }
}