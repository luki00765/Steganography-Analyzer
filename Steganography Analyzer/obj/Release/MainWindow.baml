   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   Steganography Analyzer'   !Steganography_Analyzer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro       � Title$  Steganography Analyzer��$	��500��$	��665�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ Manual=�
 � Top$ 50�� � Left$	 500�� � WindowStyle$ 
ToolWindow=�(   "MahApps.Metro.Controls.MetroWindow   ShowCloseButton .  #��� ��   EnableDWMDropShadow .     NonActiveWindowTitleBrush# � ��
�������	   WindowTitleBrush	 �	#FF1C9362.��� 
 � RowDefinitions
 �� $��33 ��� $	��328 ��� $��2* ��� $��40 � � ColumnDefinitions �� $��* ��� $��* �.+�o� $��0��$��2��
�������$��2q����� .R�]� $��Stretch=�.@�m� $��Image��$��10,0,0,0q�.@�m� $
��Open��.@�m� -   $��Load Original Image��m� -   $��Load Target Image��m� -   $��About��m� -   $��Close��$��Right=�
���  ��$��0,0,80,2q��� $��0��$��0��$��2��$��Bottom=��� $��1��$��1��
�������$��2q�$��2��$
��Left=�$��	0,-3,0,-3q� � LayoutTransform ��  �� Angle$ 90Q��� $��2��$��0��$��2��$	��Top=��� $��4��$��0��$��2��$	��Top=��� $��3��$��1��
�������$��2q�$��2��$
��Left=�$��0,3,0,0q� �� $ 90Q��� $��1��$��0��$��1q����	#FF000000$	��290��$	��290��$	��Top=�$��Right=�$��
20,20,20,0q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   Color$
 Gray��   	Direction$	 235Q�   Opacity$ .5Q�   RenderingBias$ Quality=�   ShadowDepth$ 5Q�._��� -    ��Name$ Image1��$
��Fill=����� �� $��1��$��1��$��1q����	#FF000000$	��290��$	��290��$	��Top=�$��Right=�$��
20,20,20,0q�   $ 10Q�$
 Gray��$	 235Q�$ .5Q�$ Quality=�$ 5Q�._��� -   $ Image2��$
��Fill=�$��Right=����� �� -    ��Name$ Compare��$��2��$��Compare��$	��100��$��Center=�$	��Top=�$��0,21,0,0q��� -    ��Name$ DifferentPixels��$��2�� �� TextAlignment$ Center=�$
��Auto��$��20��$��0,10,0,0q�$��15(�$
��Bold"� J� Opacity$	 0.0Q�$y�	Collapsed=��� -	   $
 PSNR��$��2��$ Center=�$
��Auto��$��20��$��0,45,0,0q�$��15(�$
��Bold"�$	 0.0Q�$y�	Collapsed=��� -
    ��Name$ BorderToDifferentImage��$��2��$��1��$��1q����	#FF000000$	��290��$	��290��$	��Top=�$��Right=�$��
20,20,20,0q�$y�	Collapsed=�   $ 10Q�$
 Gray��$	 235Q�$ .5Q�$ Quality=�$ 5Q�._��� -   $ DifferentImage��$
��Fill=�$��Right=�����  �� Mode$ OneWay=��� $��4��$��1��$��Łukasz Paczyński��$��Center=�$��Center=�$��20(�$��Buxton Sketch)��� -   $ 	SaveImage��$��2��$	��100��$��25��$��	0,100,0,0q�$��
Save Image��$	 0.0Q�$y�	Collapsed=��� -   $ Clear��$��2��$	��100��$��25��$��	0,275,0,0q�$��Clear��$y�	Collapsed=��� -   $ originalImage��$��1��$��Center=�$	��Top=�$��Original Image:��$
��Bold"��� -   $ targetImage��$��1��$��1��$��Center=�$	��Top=�$��Target Image:��$
��Bold"��� -   $ prompt��$��3��$ Center=�$��Stretch=�$��Center=�$
��Bold"�$
��Wrap=�