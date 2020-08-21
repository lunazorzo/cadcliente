--Criar o banco de dados
CREATE DATABASE mydb
  WITH OWNER = postgres
       ENCODING = 'WIN1252'
       TEMPLATE template0
       TABLESPACE = pg_default
       LC_COLLATE = 'Portuguese_Brazil.1252'
       LC_CTYPE = 'Portuguese_Brazil.1252'
       CONNECTION LIMIT = -1;

--Cria o usu�rio com a senha j� definida. Senha: minhasenha
--Se tentar usar outro usu�rio e senha o sistema n�o ir� funcionar
CREATE ROLE myuser LOGIN
  ENCRYPTED PASSWORD 'md5af1adb1c7b135733540d663e8503303f'
  SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;