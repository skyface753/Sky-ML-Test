using System;
using Unity.MLAgents.Sensors;

namespace Unity.MLAgentsExamples
{
    /// <summary>
    /// A simple example of a SensorComponent.
    /// This should be added to the same GameObject as the BasicController
    /// </summary>
    public class BasicSensorComponent : SensorComponent
    {
        public PlayerController playerController;

        /// <summary>
        /// Creates a BasicSensor.
        /// </summary>
        /// <returns></returns>
        public override ISensor[] CreateSensors()
        {
            return new ISensor[] { new BasicSensor(playerController) };
        }
    }

    /// <summary>
    /// Simple Sensor implementation that uses a one-hot encoding of the Agent's
    /// position as the observation.
    /// </summary>
    public class BasicSensor : SensorBase
    {
        public PlayerController playerController;

        public BasicSensor(PlayerController controller)
        {
            playerController = controller;
        }

        /// <summary>
        /// Generate the observations for the sensor.
        /// In this case, the observations are all 0 except for a 1 at the position of the agent.
        /// </summary>
        /// <param name="output"></param>
        public override void WriteObservation(float[] output)
        {
            // One-hot encoding of the position
            Array.Clear(output, 0, output.Length);
            output[playerController.positionX] = 1;
        }

        /// <inheritdoc/>
        public override ObservationSpec GetObservationSpec()
        {
            return ObservationSpec.Vector(PlayerController.k_Extents);
        }

        /// <inheritdoc/>
        public override string GetName()
        {
            return "Basic";
        }

    }
}
