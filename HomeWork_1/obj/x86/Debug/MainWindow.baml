   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5        
HomeWork_1   HomeWork_1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5       -   5          � Title$  
MainWindow��5   	   $	��720��6   $
��1280��6)   .��5      �� .+�5   
   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  -   5        $��kinectSensorChooser1��5   @    �� Left$ 0��6!   
 �� Top$ 0��61   $	��253��6\   $	��105��6h   �� 5   
   -   5        $��title��5   \   $ 0��6   $ 75��6$   $��Choose Your Exercise��64   $��64��6P   $
��1258��6i    �� TextAlignment$ Center=�6v   $��40(�6�   �� 5	   
   -   5        $��stackPanel1��5	   D   $ 50��6   $	 175��6&   $	��500��67   $
��1150��6W    �� Orientation$ Vertical=�6d   .+�5
      �� -   5        $��stackPanel2��5
      $ 
Horizontal=�6,   .+�5      �� -   5        $��button1��5   '   $	��200��6   $	��250��66   .��5      �� -   5        $��label1��5   <   $��Stretch��6   $��40(�6.   6<   5      �� 5      -   5        $��
separator1��5   '   $��5��6   $	��200��69    J� Opacity$ 0Q�6E   �� 5      -	   5        $��button2��5   &   $	��200��6   $	��250��65   .��5      �� -
   5        $��label2��5   @   $��Barbell Row��6   $��40(�62   6@   5      �� 5      -   5        $��
separator2��5   '   $��5��6   $	��200��69   $ 0Q�6E   �� 5      -   5        $��button3��5   &   $	��200��6   $	��250��65   .��5      �� -   5        $��label3��5   =   $��Deadlift��6   $��40(�6/   6=   5      5      �� 5      $��50��6   $��5��6$   $ 0Q�6.   �� 5      -   5        $��stackPanel3��5      $ 
Horizontal=�6,   .+�5      �� -   5        $��button4��5   &   $	��200��6   $	��250��65   .��5      �� -   5        $��label4��5   @   $��Bench Press��6   $��40(�62   6@   5      �� 5      -   5        $��
separator3��5   '   $��5��6   $	��200��69   $ 0Q�6E   �� 5      -   5        $��button5��5   &   $	��200��6   $	��250��65   .��5      �� -   5        $��label5��5   C   $��Overhead Press��6   $��40(�65   6C   5      �� 5       -   5        $��
separator4��5    '   $��5��6   $	��200��69   $ 0Q�6E   �� 5!      -   5        $��button6��5!   &   $	��200��6   $	��250��65   .��5"      �� -   5        $��label6��5"   :   $��Squat��6   $��40(�6,   6:   5#      5$      5%      �� 5&   
   -   5        $��back_button��5&   @    �� Right$ 10��6   $ 10��6#   $	��150��63   $	��175��6S   $ 0Q�6_   .��5'      �� -   5        $��back_button_label��5'   3   $��Finish��6   $��40(�6%   63   5(      A� 5)   
   -   5        $��rightEllipse��5)   @   $	 418��6   $ 30��6$   $��30��64   
���   �$��30��6c   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  5*   
   -   5        $��kinectColorViewer1��5*   B   $	 576��6   $	 149��61   $	��640��6t   $	��480��6�   $ 0Q�6�    � DataContext 6\   �� 5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6�   �� 	 �� ElementName$	 kinectSensorChooser1��
 �� Path$
 Kinect�6�   5+      5,      