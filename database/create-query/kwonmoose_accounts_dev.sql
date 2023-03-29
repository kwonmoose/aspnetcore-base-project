DROP SCHEMA IF EXISTS `kwonmoose_accounts_dev`;
CREATE SCHEMA `kwonmoose_accounts_dev`;
USE `kwonmoose_accounts_dev`;


# region 회원 테이블
DROP TABLE IF EXISTS `kwonmoose_accounts_dev`.`user`;
CREATE TABLE `kwonmoose_accounts_dev`.`user`
(

    `id`           INT AUTO_INCREMENT                NOT NULL COMMENT '회원 ID(UUID)',
    `first_name`   VARCHAR(100)                      NOT NULL COMMENT '이름',
    `last_name`    VARCHAR(100)                      NOT NULL COMMENT '성',
    `country`      VARCHAR(5)                        NOT NULL COMMENT '국가 코드(ISO 3166-1 alpha-2)',
    `gender`       CHAR(1)                           NOT NULL COMMENT '성별',
    `updated_time` BIGINT DEFAULT (UNIX_TIMESTAMP()) NOT NULL COMMENT '수정 시간(unix time)',
    `signup_time`  BIGINT DEFAULT (UNIX_TIMESTAMP()) NOT NULL COMMENT '가입 시간(unix time)',
    PRIMARY KEY (`id`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4 COMMENT ='회원 테이블';
# endregion


create user 'kwonmoose_dev'@'%' identified by 'kwonmoose_dev_1234';
grant all privileges on kwonmoose_accounts_dev.* to 'kwonmoose_dev'@'%';