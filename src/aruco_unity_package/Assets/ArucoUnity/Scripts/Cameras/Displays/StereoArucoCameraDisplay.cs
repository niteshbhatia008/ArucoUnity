﻿using ArucoUnity.Cameras;
using ArucoUnity.Cameras.Undistortions;
using UnityEngine;

namespace ArucoUnity
{
  /// \addtogroup aruco_unity_package
  /// \{

  namespace Cameras.Displays
  {
    /// <summary>
    /// Displays a <see cref="StereoArucoCamera"/>.
    /// </summary>
    public class StereoArucoCameraDisplay : ArucoCameraGenericDisplay
    {
      // Editor fields

      [SerializeField]
      [Tooltip("The camera system to use.")]
      private StereoArucoCamera stereoArucoCamera;

      [SerializeField]
      [Tooltip("The optional undistortion process associated with the ArucoCamera.")]
      private ArucoCameraUndistortion arucoCameraUndistortion;

      [SerializeField]
      [Tooltip("The container of the leftCamera and the leftBackgroundCamera.")]
      private Transform leftEye;

      [SerializeField]
      [Tooltip("The container of the rightCamera and the rightBackgroundCamera.")]
      private Transform rightEye;

      [SerializeField]
      [Tooltip("The Unity virtual camera that will shoot the 3D content aligned with the left background.")]
      private Camera leftCamera;

      [SerializeField]
      [Tooltip("The Unity virtual camera that will shoot the 3D content aligned with the right background.")]
      private Camera rightCamera;

      [SerializeField]
      [Tooltip("The Unity virtual camera that will shoot the left eye background.")]
      private Camera leftBackgroundCamera;

      [SerializeField]
      [Tooltip("The Unity virtual camera that will shoot the right eye background.")]
      private Camera rightBackgroundCamera;

      [SerializeField]
      [Tooltip("The background displaying the image of the left physical camera in ArucoCamera.")]
      private Renderer leftBackground;

      [SerializeField]
      [Tooltip("The background displaying the image of the right physical camera in ArucoCamera.")]
      private Renderer rightBackground;

      // ArucoCameraController properties

      public override IArucoCamera ArucoCamera { get { return stereoArucoCamera; } }

      // ArucoCameraGenericDisplay properties

      public override IArucoCameraUndistortion ArucoCameraUndistortion { get { return arucoCameraUndistortion; } }

      // Properties

      /// <summary>
      /// Gets or sets the camera system to use.
      /// </summary>
      public StereoArucoCamera StereoArucoCamera { get { return stereoArucoCamera; } set { stereoArucoCamera = value; } }

      /// <summary>
      /// Gets or sets the optional undistortion process associated with the ArucoCamera.
      /// </summary>
      public ArucoCameraUndistortion ConcreteArucoCameraUndistortion { get { return arucoCameraUndistortion; } set { arucoCameraUndistortion = value; } }

      /// <summary>
      /// Gets or sets the containers of the <see cref="ArucoCameraGenericDisplay{T}.Cameras"/> and the
      /// <see cref="ArucoCameraGenericDisplay{T}.BackgroundCameras"/>.
      /// </summary>
      public Transform[] Eyes { get; set; }

      // MonoBehaviour methods

      /// <summary>
      /// Populates <see cref="ArucoCameraGenericDisplay{T}.Cameras"/>, <see cref="ArucoCameraGenericDisplay{T}.BackgroundCameras"/> and
      /// <see cref="ArucoCameraGenericDisplay{T}.Backgrounds"/> from editor fields if they are set.
      /// </summary>
      protected override void Awake()
      {
        base.Awake();
        if (Eyes == null)
        {
          Eyes = new Transform[ArucoCamera.CameraNumber];
        }
        if (leftEye != null)
        {
          Eyes[StereoArucoCamera.CameraId1] = leftEye;
        }
        if (rightEye != null)
        {
          Eyes[StereoArucoCamera.CameraId2] = rightEye;
        }

        if (leftCamera != null)
        {
          Cameras[StereoArucoCamera.CameraId1] = leftCamera;
        }
        if (rightCamera != null)
        {
          Cameras[StereoArucoCamera.CameraId2] = rightCamera;
        }
        if (leftBackgroundCamera != null)
        {
          BackgroundCameras[StereoArucoCamera.CameraId1] = leftBackgroundCamera;
        }
        if (rightBackgroundCamera != null)
        {
          BackgroundCameras[StereoArucoCamera.CameraId2] = rightBackgroundCamera;
        }
        if (leftBackground != null)
        {
          Backgrounds[StereoArucoCamera.CameraId1] = leftBackground;
        }
        if (rightBackground != null)
        {
          Backgrounds[StereoArucoCamera.CameraId2] = rightBackground;
        }
      }

      // Methods

      protected override void ConfigureCamerasBackground()
      {
        base.ConfigureCamerasBackground();

        if (ArucoCameraUndistortion != null)
        {
          var stereoCameraParameters = ArucoCameraUndistortion.CameraParameters.StereoCameraParameters;

          Eyes[StereoArucoCamera.CameraId1].transform.localPosition = stereoCameraParameters.TranslationVector.ToPosition();

          // TODO rotation
        }
      }
    }
  }

  /// \} aruco_unity_package
}