using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using LSL;

    public class LSLOutput : MonoBehaviour
    {
        private LSL.StreamOutlet outlet;
        private float[] currentSample;

        public string StreamName = "Unity.ExampleStream";
        public string StreamType = "Unity.StreamType";
        public string StreamId = "MyStreamID-Unity1234";

        // Start is called before the first frame update
        void Start()
        {
            LSL.StreamInfo streamInfo = new LSL.StreamInfo(StreamName, StreamType, 3, Time.fixedDeltaTime * 1000, LSL.channel_format_t.cf_float32);
            LSL.XMLElement chans = streamInfo.desc().append_child("channels");
            chans.append_child("channel").append_child_value("label", "X");
            chans.append_child("channel").append_child_value("label", "Y");
            chans.append_child("channel").append_child_value("label", "Z");
            outlet = new LSL.StreamOutlet(streamInfo);
            currentSample = new float[3];
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 pos = gameObject.transform.position;
            currentSample[0] = pos.x;
            currentSample[1] = pos.y;
            currentSample[2] = pos.z;
            outlet.push_sample(currentSample);
        }
    }