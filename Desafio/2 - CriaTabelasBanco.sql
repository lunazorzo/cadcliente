CREATE TABLE public.usuario
(
  cd_usuario character varying(3) NOT NULL,
  nm_usuario character varying(20) NOT NULL,
  sn_usuario character varying(10) NOT NULL,
  ds_email character varying(50),
  ds_senha character varying(20),
  ds_smtp character varying(50),
  nr_porta integer DEFAULT 587,
  st_ativo boolean NOT NULL,
  st_ssl boolean NOT NULL DEFAULT true,
  dt_registro timestamp without time zone NOT NULL,
  cd_modulo character varying(3),
  cd_programa integer,
  CONSTRAINT usuario_pk PRIMARY KEY (cd_usuario)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.usuario
  OWNER TO postgres;
  
CREATE TABLE public.modulo
(
  cd_modulo character varying(3) NOT NULL,
  ds_modulo character varying(30) NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  CONSTRAINT modulo_pk PRIMARY KEY (cd_modulo)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.modulo
  OWNER TO postgres;
  
CREATE TABLE public.programa
(
  cd_modulo character varying(3) NOT NULL,
  cd_programa integer NOT NULL,
  cd_submenu integer,
  ds_programa character varying(30) NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  nm_form text,
  CONSTRAINT programa_pk PRIMARY KEY (cd_modulo, cd_programa),
  CONSTRAINT modulo_programa_fk FOREIGN KEY (cd_modulo)
      REFERENCES public.modulo (cd_modulo) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.programa
  OWNER TO postgres;
  
CREATE TABLE public.submenu
(
  cd_modulo character varying(3) NOT NULL,
  cd_submenu integer NOT NULL,
  ds_submenu character varying(10) NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  CONSTRAINT submenu_pk PRIMARY KEY (cd_modulo, cd_submenu),
  CONSTRAINT modulo_submenu_fk FOREIGN KEY (cd_modulo)
      REFERENCES public.modulo (cd_modulo) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.submenu
  OWNER TO postgres;
  
CREATE TABLE public.acessoprograma
(
  cd_usuario character varying(3) NOT NULL,
  cd_modulo character varying(3) NOT NULL,
  cd_programa integer NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  CONSTRAINT programa_acessoprograma_fk FOREIGN KEY (cd_modulo, cd_programa)
      REFERENCES public.programa (cd_modulo, cd_programa) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT usuario_acessoprograma_fk FOREIGN KEY (cd_usuario)
      REFERENCES public.usuario (cd_usuario) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.acessoprograma
  OWNER TO postgres;

CREATE TABLE public.usuarioprogmenu
(
  cd_usuario character varying(3) NOT NULL,
  cd_modulo character varying(3) NOT NULL,
  cd_programa integer NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  CONSTRAINT usuarioprogmenu_pk PRIMARY KEY (cd_usuario, cd_modulo, cd_programa),
  CONSTRAINT modulo_usuarioprogmenu_fk FOREIGN KEY (cd_modulo)
      REFERENCES public.modulo (cd_modulo) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT programa_usuarioprogmenu_fk FOREIGN KEY (cd_modulo, cd_programa)
      REFERENCES public.programa (cd_modulo, cd_programa) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT usuario_usuarioprogmenu_fk FOREIGN KEY (cd_usuario)
      REFERENCES public.usuario (cd_usuario) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.usuarioprogmenu
  OWNER TO postgres;


CREATE TABLE public.listavalores
(
  nr_sequencial integer NOT NULL,
  ds_titulo character varying(200) NOT NULL,
  nr_altura integer NOT NULL,
  nr_largura integer NOT NULL,
  ds_instrucaosql character varying(10000) NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  CONSTRAINT listavalores_pk PRIMARY KEY (nr_sequencial)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.listavalores
  OWNER TO postgres;
  
CREATE TABLE public.listavalorescolunas
(
  nr_sequencial integer NOT NULL,
  nr_coluna integer NOT NULL,
  ds_titulocoluna character varying(200) NOT NULL,
  nm_campoinstrsql character varying(200) NOT NULL,
  nr_posicao integer NOT NULL,
  nr_larguracampo integer NOT NULL,
  ds_alinhamentocampo character varying(60) NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  CONSTRAINT listavalorescolunas_pk PRIMARY KEY (nr_sequencial, nr_coluna),
  CONSTRAINT listavalores_listavalorescolunas_fk FOREIGN KEY (nr_sequencial)
      REFERENCES public.listavalores (nr_sequencial) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.listavalorescolunas
  OWNER TO postgres;
  
  
CREATE TABLE public.cliente
(
  cd_cliente integer NOT NULL,
  ds_nome character varying(200) NOT NULL,
  dt_registro timestamp without time zone NOT NULL,
  nr_telefone character varying(14),
  nr_celular character varying(14),
  nr_celular2 character varying(14),
  ds_email character varying(100),  
  CONSTRAINT cliente_pk PRIMARY KEY (cd_cliente)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.cliente
  OWNER TO postgres;
  
CREATE TABLE public.clientecontato
(
  cd_cliente integer NOT NULL,
  cd_contato integer NOT NULL,
  ds_contato character varying(200),
  ds_email character varying(200),
  nr_telefone character varying(20),
  nr_celular character varying(20),
  dt_registro timestamp without time zone NOT NULL,
  nr_celular2 character varying(14),
  CONSTRAINT clientecontato_pk PRIMARY KEY (cd_cliente, cd_contato),
  CONSTRAINT cliente_clientecontato_fk FOREIGN KEY (cd_cliente)
      REFERENCES public.cliente (cd_cliente) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.clientecontato
  OWNER TO postgres;
