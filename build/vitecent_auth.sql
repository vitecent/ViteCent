/*
 Navicat Premium Data Transfer

 Source Server         : mysql115
 Source Server Type    : MySQL
 Source Server Version : 50621
 Source Host           : 192.168.0.115:3306
 Source Schema         : vitecent_auth

 Target Server Type    : MySQL
 Target Server Version : 50621
 File Encoding         : 65001

 Date: 01/04/2025 13:53:06
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for base_company
-- ----------------------------
DROP TABLE IF EXISTS `base_company`;
CREATE TABLE `base_company`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `parentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父级标识',
  `level` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '级别',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `logo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商标',
  `country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '国家',
  `province` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '省份',
  `city` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '城市',
  `address` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细地址',
  `establishDate` date NULL DEFAULT NULL COMMENT '成立日期',
  `industry` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '行业',
  `legalPerson` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '法人',
  `legalPhone` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '法人电话',
  `email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `name`(`name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '公司信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_company
-- ----------------------------
INSERT INTO `base_company` VALUES ('1', NULL, NULL, 'OMDX', '鼎新基地', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2025-04-01 11:57:28', NULL, '2025-04-01 11:57:28', '2025-04-01 11:57:28', 1);

-- ----------------------------
-- Table structure for base_department
-- ----------------------------
DROP TABLE IF EXISTS `base_department`;
CREATE TABLE `base_department`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `parentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父级标识',
  `level` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '级别',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `manager` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '负责人',
  `managerPhone` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '负责人电话',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '部门信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_department
-- ----------------------------
INSERT INTO `base_department` VALUES ('1', NULL, NULL, '1', 'OMDX', '航气处', NULL, NULL, NULL, NULL, NULL, NULL, '2025-04-01 11:58:23', NULL, '2025-04-01 11:58:23', '2025-04-01 11:58:23', 1);

-- ----------------------------
-- Table structure for base_operation
-- ----------------------------
DROP TABLE IF EXISTS `base_operation`;
CREATE TABLE `base_operation`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `systemId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系统标识',
  `resourceId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系统标识',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `code`(`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '操作信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_operation
-- ----------------------------
INSERT INTO `base_operation` VALUES ('1', '1', '1', '1', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('10', '1', '1', '4', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('11', '1', '1', '5', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('12', '1', '1', '6', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('13', '1', '1', '7', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('14', '1', '1', '8', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('15', '1', '1', '9', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('16', '1', '1', '1', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('17', '1', '1', '10', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('18', '1', '2', '11', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('19', '1', '2', '12', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('2', '1', '1', '10', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('20', '1', '2', '13', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('21', '1', '2', '14', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('22', '1', '2', '15', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('23', '1', '1', '2', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('24', '1', '1', '3', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('25', '1', '1', '4', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('26', '1', '1', '5', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('27', '1', '1', '6', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('28', '1', '1', '7', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('29', '1', '1', '8', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('3', '1', '2', '11', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('30', '1', '1', '9', 'Edit', '修改', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('31', '1', '1', '1', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('32', '1', '1', '10', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('33', '1', '2', '11', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('34', '1', '2', '12', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('35', '1', '2', '13', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('36', '1', '2', '14', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('37', '1', '2', '15', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('38', '1', '1', '2', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('39', '1', '1', '3', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('4', '1', '2', '12', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('40', '1', '1', '4', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('41', '1', '1', '5', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('42', '1', '1', '6', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('43', '1', '1', '7', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('44', '1', '1', '8', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('45', '1', '1', '9', 'Get', '查询', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('46', '1', '1', '10', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('47', '1', '2', '11', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('48', '1', '2', '12', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('49', '1', '2', '13', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('5', '1', '2', '13', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('50', '1', '2', '14', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('51', '1', '2', '15', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('52', '1', '1', '2', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('53', '1', '1', '3', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('54', '1', '1', '4', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('55', '1', '1', '5', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('57', '1', '1', '6', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('58', '1', '1', '7', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('59', '1', '1', '8', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('6', '1', '2', '14', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('60', '1', '1', '9', 'Delete', '删除', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('7', '1', '2', '15', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('8', '1', '1', '2', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_operation` VALUES ('9', '1', '1', '3', 'Add', '新增', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);

-- ----------------------------
-- Table structure for base_position
-- ----------------------------
DROP TABLE IF EXISTS `base_position`;
CREATE TABLE `base_position`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `level` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '级别',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '职位信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_position
-- ----------------------------
INSERT INTO `base_position` VALUES ('1', NULL, '1', 'OMDX', '值班员', NULL, NULL, NULL, NULL, '2025-04-01 11:58:40', NULL, '2025-04-01 11:58:56', '2025-04-01 11:58:56', 1);

-- ----------------------------
-- Table structure for base_resource
-- ----------------------------
DROP TABLE IF EXISTS `base_resource`;
CREATE TABLE `base_resource`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `level` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '级别',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `systemId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系统标识',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `code`(`code`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '资源信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_resource
-- ----------------------------
INSERT INTO `base_resource` VALUES ('1', NULL, '1', '1', 'BaseCompany', '公司', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('10', NULL, '1', '1', 'BaseOperation', '操作', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('11', NULL, '1', '2', 'ScheduleType', '排班类型', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('12', NULL, '1', '2', 'Schedule', '排班', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('13', NULL, '1', '2', 'ShiftSchedule', '换班', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('14', NULL, '1', '2', 'RepairSchedule', '补卡', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('15', NULL, '1', '2', 'UserLeave', '请假', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('2', NULL, '1', '1', 'BaseDepartment', '部门', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('3', NULL, '1', '1', 'BasePosition', '职位', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('4', NULL, '1', '1', 'BaseRole', '角色', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('5', NULL, '1', '1', 'BaseRolePermission', '角色权限', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('6', NULL, '1', '1', 'BaseUser', '用户', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('7', NULL, '1', '1', 'BaseUserRole', '用户角色', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('8', NULL, '1', '1', 'BaseSystem', '系统', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_resource` VALUES ('9', NULL, '1', '1', 'BaseResource', '资源', NULL, NULL, NULL, NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);

-- ----------------------------
-- Table structure for base_role
-- ----------------------------
DROP TABLE IF EXISTS `base_role`;
CREATE TABLE `base_role`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_role
-- ----------------------------
INSERT INTO `base_role` VALUES ('1', '1', 'OMDX', '鼎新基地', NULL, NULL, NULL, NULL, '2025-04-01 12:02:15', NULL, '2025-04-01 12:02:15', '2025-04-01 12:02:15', 1);

-- ----------------------------
-- Table structure for base_role_permission
-- ----------------------------
DROP TABLE IF EXISTS `base_role_permission`;
CREATE TABLE `base_role_permission`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `roleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色标识',
  `systemId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系统标识',
  `resourceId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '资源标识',
  `operationId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '操作标识',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE,
  INDEX `roleId`(`roleId`) USING BTREE,
  INDEX `resourceId`(`resourceId`) USING BTREE,
  INDEX `operationId`(`operationId`) USING BTREE,
  INDEX `systemId`(`systemId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色权限' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_role_permission
-- ----------------------------
INSERT INTO `base_role_permission` VALUES ('1', '1', '1', '1', '1', '1', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('10', '1', '1', '1', '4', '10', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('11', '1', '1', '1', '5', '11', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('12', '1', '1', '1', '6', '12', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('13', '1', '1', '1', '7', '13', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('14', '1', '1', '1', '8', '14', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('15', '1', '1', '1', '9', '15', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('16', '1', '1', '1', '1', '16', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('17', '1', '1', '1', '10', '17', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('18', '1', '1', '2', '11', '18', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('19', '1', '1', '2', '12', '19', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('2', '1', '1', '1', '10', '2', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('20', '1', '1', '2', '13', '20', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('21', '1', '1', '2', '14', '21', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('22', '1', '1', '2', '15', '22', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('23', '1', '1', '1', '2', '23', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('24', '1', '1', '1', '3', '24', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('25', '1', '1', '1', '4', '25', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('26', '1', '1', '1', '5', '26', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('27', '1', '1', '1', '6', '27', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('28', '1', '1', '1', '7', '28', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('29', '1', '1', '1', '8', '29', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('3', '1', '1', '2', '11', '3', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('30', '1', '1', '1', '9', '30', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('31', '1', '1', '1', '1', '31', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('32', '1', '1', '1', '10', '32', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('33', '1', '1', '2', '11', '33', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('34', '1', '1', '2', '12', '34', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('35', '1', '1', '2', '13', '35', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('36', '1', '1', '2', '14', '36', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('37', '1', '1', '2', '15', '37', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('38', '1', '1', '1', '2', '38', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('39', '1', '1', '1', '3', '39', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('4', '1', '1', '2', '12', '4', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('40', '1', '1', '1', '4', '40', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('41', '1', '1', '1', '5', '41', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('42', '1', '1', '1', '6', '42', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('43', '1', '1', '1', '7', '43', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('44', '1', '1', '1', '8', '44', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('45', '1', '1', '1', '9', '45', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('46', '1', '1', '1', '10', '46', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('47', '1', '1', '2', '11', '47', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('48', '1', '1', '2', '12', '48', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('49', '1', '1', '2', '13', '49', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('5', '1', '1', '2', '13', '5', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('50', '1', '1', '2', '14', '50', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('51', '1', '1', '2', '15', '51', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('52', '1', '1', '1', '2', '52', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('53', '1', '1', '1', '3', '53', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('54', '1', '1', '1', '4', '54', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('55', '1', '1', '1', '5', '55', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('57', '1', '1', '1', '6', '57', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('58', '1', '1', '1', '7', '58', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('59', '1', '1', '1', '8', '59', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('6', '1', '1', '2', '14', '6', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('60', '1', '1', '1', '9', '60', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('7', '1', '1', '2', '15', '7', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('8', '1', '1', '1', '2', '8', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_role_permission` VALUES ('9', '1', '1', '1', '3', '9', NULL, '2025-04-01 00:00:00', NULL, '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);

-- ----------------------------
-- Table structure for base_system
-- ----------------------------
DROP TABLE IF EXISTS `base_system`;
CREATE TABLE `base_system`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编码',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `abbreviation` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简称',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `name`(`name`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_system
-- ----------------------------
INSERT INTO `base_system` VALUES ('1', '1', 'Auth', '权限', '', '', NULL, '', '2025-04-01 00:00:00', '', '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);
INSERT INTO `base_system` VALUES ('2', '1', 'Basic', '排班', '', '', NULL, '', '2025-04-01 00:00:00', '', '2025-04-01 00:00:00', '2025-04-01 00:00:00', 1);

-- ----------------------------
-- Table structure for base_user
-- ----------------------------
DROP TABLE IF EXISTS `base_user`;
CREATE TABLE `base_user`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `departmentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '部门标识',
  `positionId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '职位标识',
  `userNo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
  `username` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户名',
  `password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `phone` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `realName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '真实姓名',
  `nickname` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `gender` int(11) NULL DEFAULT 1 COMMENT '性别',
  `idCard` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '身份证',
  `birthday` date NULL DEFAULT NULL COMMENT '出生日期',
  `avatar` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `description` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简介',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `username`(`username`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE,
  INDEX `userNo`(`userNo`) USING BTREE,
  INDEX `idCard`(`idCard`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_user
-- ----------------------------
INSERT INTO `base_user` VALUES ('1', '', '', '', NULL, 'admin', 'C383B86184682DF3E48B2BCE066B91E2', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '2025-04-01 11:59:40', NULL, '2025-04-01 12:00:26', '2025-04-01 12:00:26', 1);
INSERT INTO `base_user` VALUES ('2', '1', '1', '1', NULL, 'OMDX', '88E7B0065009ECF17FD3830CEBE45F11', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '2025-04-01 12:00:03', NULL, '2025-04-01 12:00:03', '2025-04-01 12:00:03', 1);

-- ----------------------------
-- Table structure for base_user_role
-- ----------------------------
DROP TABLE IF EXISTS `base_user_role`;
CREATE TABLE `base_user_role`  (
  `id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '公司标识',
  `departmentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '部门标识',
  `userId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户标识',
  `roleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色标识',
  `color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `creator` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改时间',
  `dataVersion` timestamp(0) NOT NULL DEFAULT CURRENT_TIMESTAMP(0) ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '数据版本',
  `status` int(11) NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `companyId`(`companyId`) USING BTREE,
  INDEX `departmentId`(`departmentId`) USING BTREE,
  INDEX `userId`(`userId`) USING BTREE,
  INDEX `roleId`(`roleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户角色' ROW_FORMAT = Compact;

-- ----------------------------
-- Records of base_user_role
-- ----------------------------
INSERT INTO `base_user_role` VALUES ('1', '1', '1', '1', '1', NULL, NULL, '2025-04-01 12:02:28', NULL, '2025-04-01 12:02:28', '2025-04-01 12:02:28', 1);

SET FOREIGN_KEY_CHECKS = 1;
