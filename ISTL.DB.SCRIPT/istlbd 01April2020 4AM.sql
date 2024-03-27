/*
SQLyog Enterprise - MySQL GUI v6.13
MySQL - 5.6.17 : Database - istlbd
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

create database if not exists `istlbd`;

USE `istlbd`;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

/*Table structure for table `lookup` */

DROP TABLE IF EXISTS `lookup`;

CREATE TABLE `lookup` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Code` varchar(10) DEFAULT NULL,
  `Name` varchar(30) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `ParentId` bigint(20) DEFAULT '0',
  `Status` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=latin1;

/*Data for the table `lookup` */

insert  into `lookup`(`Id`,`Code`,`Name`,`Type`,`ParentId`,`Status`) values (1,NULL,'Dhaka','PlaceOfBirth',0,1),(2,NULL,'Savar','PlaceOfBirth',0,1),(3,NULL,'Bangladeshi','Nationality',0,1),(4,NULL,'Dual Nationality','Nationality',0,1),(5,NULL,'Male','Gender',0,1),(6,NULL,'Female','Gender',0,1),(7,NULL,'Division 1','Division',0,1),(8,NULL,'Division 2','Division',0,1),(9,NULL,'Division 3','Division',0,1),(10,NULL,'District 1 (Division 1)','District',7,1),(11,NULL,'District 2 (Division 1)','District',7,1),(12,NULL,'District 3 (Division 2)','District',8,1),(13,NULL,'District 4 (Division 2)','District',8,1),(14,NULL,'District 5 (Division 3)','District',9,1),(15,NULL,'Station 1 (District 1)','Station',10,1),(16,NULL,'Station 2 (District 1)','Station',10,1),(17,NULL,'Station 3 (District 2)','Station',11,1),(18,NULL,'Station 4 (District 3)','Station',12,1),(19,NULL,'Upazila 1 (Station 1)','Upazila',15,1),(20,NULL,'Upazila 2 (Station 1)','Upazila',15,1),(21,NULL,'Upazila 3 (Station 2)','Upazila',16,1),(22,NULL,'Upazila 4 (Station 3)','Upazila',17,1),(23,NULL,'Union 1 (Upazila 1)','Union',19,1),(24,NULL,'Union 2 (Upazila 1)','Union',19,1),(25,NULL,'Union 3 (Upazila 2)','Union',20,1),(26,NULL,'Union 4 (Upazila 3)','Union',21,1),(27,NULL,'PostCode 1 (Union 1)','PostCode',23,1),(28,NULL,'PostCode 2 (Union 1)','PostCode',23,1),(29,NULL,'PostCode 3 (Union 2)','PostCode',24,1),(30,NULL,'PostCode 4 (Union 3)','PostCode',25,1),(31,NULL,'Application Status 1 ','ApplicationStatus',0,1),(32,NULL,'Application Status 2','ApplicationStatus',0,1),(33,NULL,'A+','BloodGroup',0,1),(34,NULL,'B+','BloodGroup',0,1),(35,NULL,'O+','BloodGroup',0,1),(36,NULL,'AB-','BloodGroup',0,1);

/*Table structure for table `personenrollment` */

DROP TABLE IF EXISTS `personenrollment`;

CREATE TABLE `personenrollment` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `FirstNameEn` varchar(50) DEFAULT NULL,
  `MiddleNameEn` varchar(50) DEFAULT NULL,
  `LastNameEn` varchar(50) DEFAULT NULL,
  `FirstNameLocal` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `MiddleNameLocal` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastNameLocal` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `PlaceOfBirth` tinyint(4) DEFAULT NULL,
  `NationalityId` tinyint(4) DEFAULT NULL,
  `DateOfBirth` date DEFAULT NULL,
  `Gender` tinyint(4) DEFAULT NULL,
  `MotherName` varchar(100) DEFAULT NULL,
  `FatherName` varchar(100) DEFAULT NULL,
  `SpouseName` varchar(100) DEFAULT NULL,
  `MaritalStatus` tinyint(4) DEFAULT NULL,
  `Occupation` tinyint(4) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `MobileNumber` varchar(20) DEFAULT NULL,
  `PermanentDivisionId` tinyint(4) DEFAULT NULL,
  `PermanentDistrictId` tinyint(4) DEFAULT NULL,
  `PermanentStationId` smallint(6) DEFAULT NULL,
  `PermanentUpazilaId` smallint(6) DEFAULT NULL,
  `PermanentUnionId` mediumint(9) DEFAULT NULL,
  `PermanentPostCode` smallint(6) DEFAULT NULL,
  `PermanentAddress` text,
  `PresentDivisionId` tinyint(4) DEFAULT NULL,
  `PresentDistrictId` tinyint(4) DEFAULT NULL,
  `PresentStationId` smallint(6) DEFAULT NULL,
  `PresentPostCode` smallint(6) DEFAULT NULL,
  `PresentAddress` text,
  `Status` varchar(255) DEFAULT NULL,
  `ApplicationStatus` tinyint(4) DEFAULT NULL,
  `BloodGroupId` tinyint(4) DEFAULT NULL,
  `CategoriesId` varchar(255) DEFAULT NULL,
  `OrganizationId` varchar(255) DEFAULT NULL,
  `Designation` varchar(255) DEFAULT NULL,
  `Remarks` text,
  `Photo` longblob,
  `PhotoUrl` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `personenrollment` */

insert  into `personenrollment`(`Id`,`FirstNameEn`,`MiddleNameEn`,`LastNameEn`,`FirstNameLocal`,`MiddleNameLocal`,`LastNameLocal`,`PlaceOfBirth`,`NationalityId`,`DateOfBirth`,`Gender`,`MotherName`,`FatherName`,`SpouseName`,`MaritalStatus`,`Occupation`,`Email`,`MobileNumber`,`PermanentDivisionId`,`PermanentDistrictId`,`PermanentStationId`,`PermanentUpazilaId`,`PermanentUnionId`,`PermanentPostCode`,`PermanentAddress`,`PresentDivisionId`,`PresentDistrictId`,`PresentStationId`,`PresentPostCode`,`PresentAddress`,`Status`,`ApplicationStatus`,`BloodGroupId`,`CategoriesId`,`OrganizationId`,`Designation`,`Remarks`,`Photo`,`PhotoUrl`) values (1,'Mr.','Al-Amin','Chowdhury','????? ???????',NULL,'????? ???????',1,4,'1988-01-30',5,NULL,NULL,NULL,NULL,NULL,NULL,'01711390910',7,10,15,19,23,28,'Flat B2, House 285, Road 4, Mirpur DOHS ',NULL,NULL,NULL,NULL,NULL,NULL,32,34,NULL,NULL,NULL,NULL,'����\0JFIF\0\0`\0`\0\0��\0ZExif\0\0MM\0*\0\0\0\0\0\0\0\0\0\0\0J\0\0\0\0\0\0\0\0Q\0\0\0\0\0\0\0Q\0\0\0\0\0\0�Q\0\0\0\0\0\0�\0\0\0\0\0��\0\0����\0C\0		\n\r\Z\Z $.\' \",#(7),01444\'9=82<.342��\0C			\r\r2!!22222222222222222222222222222222222222222222222222��\0\0�\0�\"\0��\0\0\0\0\0\0\0\0\0\0\0	\n��\0�\0\0\0}\0!1AQa\"q2���#B��R��$3br�	\n\Z%&\'()*456789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz���������������������������������������������������������������������������\0\0\0\0\0\0\0\0	\n��\0�\0\0w\0!1AQaq\"2�B����	#3R�br�\n$4�%�\Z&\'()*56789:CDEFGHIJSTUVWXYZcdefghijstuvwxyz��������������������������������������������������������������������������\0\0\0?\0�ͣ(�=)h� 0���G���h�ھ���������G$���y�{{\n�����	��( zʌ�f���?*L/���*�ƣih�n.b����\n\0���I�z\n�oh�&�ԭ�{=h[�����[̒��04c����)3J(p��)v���7<Ө\0�=�F����n��AK�}�KK@\rؾ��ؿ��(�ȣi���\'�4S@-QH�(�����\nBh5��k6�-�\\^I�G@:���H�֩ed��.#@:�ג��\0.�e�?g�\0��~��ۭr�bK�I�����-C���#���\Zk�\\�#כ�j��\\�᝘��j�G�(�����*�\\�$k5��6y�j�a��:l�K[�����s��#���Yܲ�J�݌�i�V��Z_�����_�rŜ9�Ǩ��A��]�ꐆ��(��c�°����	��ԑ]I�l\ZBq>�G)���Ҽ;�^<�D�m�[G#+�Sԏ�d�5K]V�.��Y#oC�>�ލ�ڱ|R���KHC�-%-\0QE \Z�\0p�C��ERh���QKI@	E�Ҁ!�����I�p�F����xg�<J����m��޷~%��{�D���l��.��n��\n����f��@\r\'��#dM�B����d�\0*��y,B���5{��B�O�J2���zV��\0\0�\\�+�#����y�zԋ���~�����&Mz�Z!����E?pV.��<��������.�F��^��P�Ɍ늫.���j�&]I��\"k�\0�j�u�\r�6\\C^�.��#�VV���<�=8��%�\Z/c���~�Z��1�[�\n�O��#\'���W3h�Wl����X�V��֚z�2����Ή�Z�+ul�OU>��������T���f_��\r{u����qn��q�j�y+2Х��p�HR�R�_�\Z(�h��){�R�\0����\06���-��C��K1�5=s�9���f�˜���d�C�kVx��Mw�\\M#��9bs�sY��l��)&l�zU�8�\\��&�N�ꦯ+΁l �DQںXc�b�JJqֺq��*n�=Yh�1Ş*q�:1Ҧϵm+�2,�)]�p}��UȅvR�jϸ�*Emɂ*��\n�8[a�U�\0��9�#�`G���ۇ���^r��׌pq]t�1��^帛 �#?08�^��^D�TΠ��!�O�x�yVP�lu�_��y~+���,:t��](�ǽ\nu �J)�KIK@\r�h��CE4\0)i)i\0QIE\0��i˫h��@��*	�c��)�[�*_B`��&��b�^� y�B����5�I��.Rua\"����]�,�DҐy8氫+@�3���b*�[v�v��v����A��/j����Z���\0x���N�����#a���u�\\=��nZEW�g�+��Յ��ǒx�4N�ZV����aV�i.k2�Z��Ic�u�섮ˬ�S�p��+\r�m$, ���5J_��&(~Q�p���@����4��m?7Q^cut�M���z�^�\r�]Ÿw�J����Q$}����(��ɮ��>{���^�����x\\�\Z3���ּ��0�I>�Z��Z\n�^��D�e.O|6��y�zp���)�KIKڐ�h��ᢩ\0R�R�\0��E-\0%!�KHzS�t��s5�2�U^x=�?*OD����I��ƶ��g�~�?\0k\'��mѭ���u�VN�� ��]QSP�m_(?xǖ�O�;��k�a���	ZC�s*�8�3�V�ņ�D���ʟCI�XZk	�����+�p~��9G���ot��L����#\r�����>��gq�\r��j�䤺|vPۈ`�:b��l�?��֦i-P&�u4eR!�lW1sl��쑞��i�6�����4��hv1<��U-X���\\��q�.YU�*FX�z�L���D\Z�C)���}�=��{X����b�Y\Z�h	\nу����R;;[=Kw��s�����Vܖ���qJ�h�PG\"��@<�XMu�e�AR+Ŗi�������\0*�?x���)�8��5�qs�%�$l���ϥ{�*���@����QI�s\n[���@���H��K�\\�����E\n-Y�R�E �\0p�J�\0t�T�%-%.)\0�����(��r~.�ᜌ����W?���Έ\'�\\���y��}�Ox��_�s�+δd[�w1�x��rV���hT�5�F��(��:ʢ��Q�X\nO5�7�RA=��`I�9�wC��J�2O&�E�Q���\0�s��-��GqZ\r�p+$��}�����q�����-�����Q��*�{յm��\'�4�vQ� �p+���om�2P3�\0*�\'}�W!s)���_	\Zm��}E[�/�W;O��{t��2�~�?ֻ1Yz\r��t�ca�o��J��G����Q�-%-Q��Qފ\0k��E�\r�Q@��(����\0C\\���[Yݛ�t�ӜHBGz��;Z����z?�TT��kJ\\�9�\r^�ȟkcҭ	�Fk��ǧ��M�}�K{�~�%	�P3��[M0<\nj�c�n7`�m�뉵.x�?�Wӭ屢e���c��D�9Zb6�9��M�KnZ�����\\m5�֣�L\'Z�-�*�ˑ�U�\rxBKk�/���T�?Lѥ���J5��q�\n�oJ\Z]��긻!E-V�����B�JZ5�ᢇ���hAE%\0QE%\0-�S\0����`��3\02N(��p���?.r>�U�I_��V|Cv�s�q1H�F�\0��Ɖ�~��Ս��z��J�\0l߽e\0zm�([mF��`l	q�U����������?�(Ill��{A������	��PM&���������hi�>��jX�M���գi!7}�Cu2��cp�:\Z�k�@�w���Z9���1M�ZI\\��1hV��i��=k��aiZ�F���\n�E<c?}�����\rw%ecȩ.i\\u�P@�KM�������频�ᢚ\0���4\0����4\0��f�Mbk^+Ҵ5asp\Z`2!��?���p�y׍<d��L����4�z���X:�ċ���+0--�c�9r>��\n�����H{�ka���\r>H��(CpG\"��Q�|��t5���q���v��=��?�_�t`ۮb\\�������=zz�#�\rZ��9�}IT��}kM58���bMb��^G�W1���}�s$��\\��c�tN��1�X;�y��]��&�F���-��f�t��:M��P�%�l���U�b8�?\nk��ƒֶ�}sI�_�r��s��\0�WyYV\\��9]M�x�O����*Ŵ���\0��}����LY�!gL,�g�?�k�~(M�\0�T�O�$�\0�r�/��t�YK���2�=��[>�\rK��-�ͤ�a�m�x2��\'�:�ֻ�\rZ�T�ͱ��t�F�>��M�sB��\nPi\0��M�-\0�\r����x��E5�V�&t�Vf���t�\0�k��^��\0\n`v[�Y�N��\'�M�i{E���\Z�mk���nE�Ȉ�\0<~g��>{�gb���I����?Q��\r��H�������{�n�&Ggf9$��P3d�jX�	��Z��+�BT;�G5,����3LG�|2Ս��l����p�y�E{\Z�_3��Z�C4d	#p�}�WѺe�_i��Q��d�l�M��4`��n�!���+\nKUaʂ+����i�wO\0�z����I�Q;hW�29�����T�gH�!@��:犚�LmZl9\"�$`~��\"���\'du�J*�A��\0��E����f5?�ч��Oһ7b�ikjaT\0�����YO;�4,^�8r+eZ���xo��s�{��*�~�5��v5��\\�d����$����L�X�}ɭ�3l�ۓ��j�g�\\�ʲ[��8���Y��\"���N¹�:G��^�t�v��z�7��?�v�gĽ\Z�*܉-\\�\0xn_��xB���RF�4�@�v�S��M֗P�?�pqV�\n���P��Ex�taє���t߈zݐU7^z��.�ׯ�K�ss�\Z+��~!���Y�[���VFʓ����a�x�P�e�sp�s�\'��+�v9�4��u&�B�=�qS�`j�~d���Yt檶���lC�ԑ_#��-�g��S�L�Vd�Z�Π{�Uf�@	+0�q�q����@�c���x`??�^���SkMl�<xQ���4>��������O:�i�76���þL�\0����f��L�0,?$|��x��W<�ӵ��ۢ]*��7n��ת���iN�s��m���!.K�,R���q�Q��\rΡΕu��0,�0T{�9#����<I�xr��3[�G�x���P�ϵI��Q�]���\0<�]=��\0U�ʖ����GK�x�/�����,�1�3�����*O^_]|�2��Mx޵������>��V�u�������|d�p~�\0�~j�2�Rz]��I�����) ��V`	��J�̇��iǑJ�ژ]��K>:c��P2X���s��֬�P������*�^1ғ\04���Q�ӗ�\0t$x�M�=h�xH�I����P�h�e��v\Z�G=)�c��B���;����@���<�0�o�U����zկ\'i��JHǭ *�vݼ��\0�҂��!y�\r>�m>�9��)*6U�*�\r)�/�xZ��N���e���EbIq�=ꇆu�K���5��PFJ�U����5�]�ja�k��F��7�b��*�ܹ�����E�&mY��i�h�<Ӎ��b�ٟ�O���ߝp�s���������>��=��\0�\Z̸���㼱����R���V����k8�/�E���:0�=28?Z\\�;�䭩GMЖ]]�//��t=(�S��\0,��?�+��,D{�a��y��g�Iy��B~C�sL���`�\"�)ǵO�Қ}���S<��X�n�ג{���\'��HHڐ�Jzb���3Ҟ7��P���jD����N(�����o�wZ)��\0��M�\0���覈��B2j3��})���5!j�0!�*�\Z�\\�0}˚�j�T��LON@��4P3�L\0�f��)NOJgs@�oh�3�D��:���K�*�ʃ�u���x\0����j�K���>�� �8?�VA�-����ofq\n6c��\0_ޛW3�w;!�Y�Y�/�m�#+�z\0EcxBM�\"��-��m��Jˎ���w��8�8U�=�Ou{�}j�Hp=��B]Yr�D{E�>˻ڼgQ���#�����9�qj�<\'uy�V������ J]B;\n�EV\0�<*������\'oZ7$����p3�Q�(\0ݩc�{�036dbr��M��,��~lzT��@\0���OC�W�F搑�xL���o�w_�EC�g�\0��L��\0���tS�3Z�#|���N\'��)rG@�Հ�Z�A��G\"��ޤ=�2294��  ��F�id����Ia=OJ3�\n���s@��L�H�JÎ��3Tl�F#��V,�y��U5���d�Ў��悰���S)lt�T�\0e�ˈ\r�8�ǹ��G����:����+[���/���G���S٨�@��NR29���\0���_��#:�h�|E�;G��tQ�h�}��%+�	���cÍ�d��U��\0du�C�}�E�ƣ2�Ɉ��K��?�h�VV��N�J~09���1��=i�6��z\nyZ��%���{Pn�=�LF=j�MW��F�H��N��zS`i����T\0�\0	�t�n��L7� Kx�L�\0���tQ��Ěi��_�E(�MM�u�D��=��LYA�	t�7u~����O�e�\01X����&iJ��������\0:o�t<r�\0�W2�B��C,$��[�\0`�wV�����:9ÔÄ3}j]�ֲ�B��.;u�����:���ϭ!�������-���ݺ���\rMM����}4��������V��!�\0k�6�-���[L��k�<��(�C�KB�߉�\r���A�ۼ|��q����OE�T�6��ǎ�s?�����\0\n��-\"�/��ZT%Sq�_��A�$��\Z�YȇN ~��rӘ�]�fY%����>��4�m]���a�?����W)��,�o�#i�:V��!�\0k�(}��)��YF��� �:q]|��?{��۩\rϽ\'SQ��UB�8�֛&Eo�>�\0�I��	���\0��\0\n@Q9�OsK�ݫ{�6����˦[�9�R��Hw��\0�K���xO�Emxj��A�2�ȝ:�z+H;�*����',NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
