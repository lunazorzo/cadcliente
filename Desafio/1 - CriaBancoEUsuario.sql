--Criar o banco de dados
CREATE DATABASE mydb
  WITH OWNER = postgres
       ENCODING = 'WIN1252'
       TEMPLATE template0
       TABLESPACE = pg_default
       LC_COLLATE = 'Portuguese_Brazil.1252'
       LC_CTYPE = 'Portuguese_Brazil.1252'
       CONNECTION LIMIT = -1;

--Cria o usuário com a senha já definida. Senha: minhasenha
--Se tentar usar outro usuário e senha o sistema não irá funcionar
CREATE ROLE myuser LOGIN
  ENCRYPTED PASSWORD 'md5af1adb1c7b135733540d663e8503303f'
  SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;