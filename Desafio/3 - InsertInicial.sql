--Lista Valores
INSERT INTO public.listavalores(nr_sequencial, ds_titulo, nr_altura, nr_largura, ds_instrucaosql, dt_registro) VALUES (1,'Usuário',400,600,'select cd_usuario, nm_usuario from usuario :pDsWhere', current_timestamp);
INSERT INTO public.listavalores(nr_sequencial, ds_titulo, nr_altura, nr_largura, ds_instrucaosql, dt_registro) VALUES (2,'Módulo',400,600,'select cd_modulo, ds_modulo from modulo :pDsWhere', current_timestamp);
INSERT INTO public.listavalores(nr_sequencial, ds_titulo, nr_altura, nr_largura, ds_instrucaosql, dt_registro) VALUES (3,'SubMenu',400,600,'select cd_modulo, cd_submenu, ds_submenu from submenu :pDsWhere', current_timestamp);
INSERT INTO public.listavalores(nr_sequencial, ds_titulo, nr_altura, nr_largura, ds_instrucaosql, dt_registro) VALUES (4,'Programa',400,600,'select cd_modulo, cd_programa, ds_programa from programa :pDsWhere', current_timestamp);
INSERT INTO public.listavalores(nr_sequencial, ds_titulo, nr_altura, nr_largura, ds_instrucaosql, dt_registro) VALUES (5,'Cliente',400,600,'select  cd_cliente, ds_nome, nr_telefone, nr_celular,  nr_celular2, ds_email from public.cliente :pDsWhere', current_timestamp);

--Lista Valores Colunas
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (1,1,'Código Usuário','cd_usuario',1,100,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (1,2,'Nome','nm_usuario',2,200,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (2,1,'Código Módulo','cd_modulo',1,100,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (2,2,'Descrição Módulo','ds_modulo',2,200,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (3,1,'Código Módulo','cd_modulo',1,100,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (3,2,'Código SubMenu','cd_submenu',2,200,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (3,3,'Descrição SubMenu','ds_submenu',2,200,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (4,1,'Cód. Módulo','cd_modulo',1,100,'Centralizado',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (4,2,'Cód. Programa','cd_programa',2,100,'Centralizado',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (4,3,'Descrição Programa','ds_programa',2,200,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (5,1,'Código','cd_cliente',1,110,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (5,2,'Nome','ds_nome',2,150,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (5,3,'Telefone','nr_telefone',3,80,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (5,4,'Celular','nr_celular',4,80,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (5,5,'Celular 2','nr_celular2',5,80,'Esquerda',current_timestamp);
INSERT INTO public.listavalorescolunas(nr_sequencial, nr_coluna, ds_titulocoluna, nm_campoinstrsql, nr_posicao, nr_larguracampo, ds_alinhamentocampo, dt_registro) VALUES (5,6,'E-mail','ds_email',6,150,'Esquerda',current_timestamp);

-- Usuário
INSERT INTO public.usuario(cd_usuario, nm_usuario, sn_usuario, nr_porta, st_ativo, st_ssl, dt_registro) VALUES ('ADM','ADMINISTRADOR','123',587,TRUE,TRUE,current_timestamp);
INSERT INTO public.usuario(cd_usuario, nm_usuario, sn_usuario, nr_porta, st_ativo, st_ssl, dt_registro) VALUES ('1','USUARIO','123',587,TRUE,TRUE,current_timestamp);

--Modulo
INSERT INTO public.modulo(cd_modulo, ds_modulo, dt_registro) VALUES ('CAD','Cadastro',current_timestamp);
INSERT INTO public.modulo(cd_modulo, ds_modulo, dt_registro) VALUES ('REL','Relatório',current_timestamp);
INSERT INTO public.modulo(cd_modulo, ds_modulo, dt_registro) VALUES ('SIS','Sistema',current_timestamp);

--Submenu
INSERT INTO public.submenu(cd_modulo, cd_submenu, ds_submenu, dt_registro) VALUES ('CAD',2,'Cadastro',current_timestamp);
INSERT INTO public.submenu(cd_modulo, cd_submenu, ds_submenu, dt_registro) VALUES ('REL',3,'Cadastro',current_timestamp);
INSERT INTO public.submenu(cd_modulo, cd_submenu, ds_submenu, dt_registro) VALUES ('SIS',1,'Sistema',current_timestamp);

--Programa
INSERT INTO public.programa(cd_modulo, cd_programa, cd_submenu, ds_programa, dt_registro) VALUES ('CAD',1,2,'Cliente',current_timestamp);
INSERT INTO public.programa(cd_modulo, cd_programa, cd_submenu, ds_programa, dt_registro) VALUES ('REL',1,3,'Cliente',current_timestamp);
INSERT INTO public.programa(cd_modulo, cd_programa, ds_programa, dt_registro) VALUES ('SIS',1,'Lista de Valores',current_timestamp);
INSERT INTO public.programa(cd_modulo, cd_programa, ds_programa, dt_registro) VALUES ('SIS',2,'Módulos',current_timestamp);
INSERT INTO public.programa(cd_modulo, cd_programa, ds_programa, dt_registro) VALUES ('SIS',3,'Permissão de Acesso',current_timestamp);

--Acesso Programa
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','CAD',1,current_timestamp);
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','REL',1,current_timestamp);
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','SIS',1,current_timestamp);
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','SIS',2,current_timestamp);
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','SIS',3,current_timestamp);
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('1','REL',1,current_timestamp);
INSERT INTO public.acessoprograma(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('1','CAD',1,current_timestamp);

--Usuario PrgramaMenu
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('1','CAD',1,current_timestamp);
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('1','REL',1,current_timestamp);
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','CAD',1,current_timestamp);
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','REL',1,current_timestamp);
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','SIS',1,current_timestamp);
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','SIS',2,current_timestamp);
INSERT INTO public.usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) VALUES ('ADM','SIS',3,current_timestamp);


