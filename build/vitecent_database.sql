/*
 Navicat Premium Dump SQL

 Source Server         : 192.168.0.9
 Source Server Type    : MySQL
 Source Server Version : 80042 (8.0.42)
 Source Host           : 192.168.0.9:3306
 Source Schema         : vitecent_database

 Target Server Type    : MySQL
 Target Server Version : 80042 (8.0.42)
 File Encoding         : 65001

 Date: 09/06/2025 09:28:02
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for base_logs
-- ----------------------------
DROP TABLE IF EXISTS `base_logs`;
CREATE TABLE `base_logs`  (
  `id` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '标识',
  `companyId` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '公司标识',
  `companyName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '公司名称',
  `departmentId` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '部门标识',
  `departmentName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '部门名称',
  `systemId` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '系统标识',
  `systemName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '系统名称',
  `resourceId` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '资源标识',
  `resourceName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '资源名称',
  `operationId` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '操作标识',
  `operationName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL COMMENT '操作名称',
  `description` varchar(5000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '简介',
  `args` text CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL COMMENT '数据',
  `creator` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `createTime` datetime NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updater` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL COMMENT '修改人',
  `updateTime` datetime NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  `dataVersion` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '数据版本',
  `status` int NULL DEFAULT 1 COMMENT '状态',
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `departmentId`(`departmentId` ASC) USING BTREE,
  INDEX `companyId`(`companyId` ASC) USING BTREE,
  INDEX `resourceId`(`resourceId` ASC) USING BTREE,
  INDEX `operationId`(`operationId` ASC) USING BTREE,
  INDEX `systemId`(`systemId` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci COMMENT = '日志信息' ROW_FORMAT = COMPACT;

SET FOREIGN_KEY_CHECKS = 1;
