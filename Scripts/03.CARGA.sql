USE [db_stock]
GO

INSERT INTO produto_tipo VALUES (1,'INSUMO','S');
INSERT INTO produto_tipo VALUES (2,'SEMI-ACABADO','S');
INSERT INTO produto_tipo VALUES (3,'ACABADO','S');
INSERT INTO produto_tipo VALUES (4,'ALUGUEL','S');
INSERT INTO produto_tipo VALUES (5,'USO E CONSUMO','S');
INSERT INTO produto_tipo VALUES (6,'ATIVO IMOBILIZADO','S');
GO


INSERT INTO produto_cor VALUES ('001','ÚNICA', 'S');
INSERT INTO produto_tamanho VALUES ('001','ÚNICO', 'S' );
INSERT INTO produto_grade VALUES ('001', 'ÚNICA','S');
INSERT INTO produto_grade_tamanho VALUES('001','001','001');
GO


INSERT INTO produto_tabela_preco VALUES ('VA', 'VAREJO', 'S');
GO

INSERT INTO perfil_de_acesso VALUES (1, 'Administrador','Administrador',NULL);
INSERT INTO perfil_de_acesso VALUES (2, 'Usuário','Usuário',NULL);
GO



INSERT INTO usuario VALUES ( 'JOSÉ','jose@gmail.com','123456');
INSERT INTO usuario VALUES ( 'JOÃO','joao@gmail.com','123456');
GO


INSERT INTO usuario_perfil_acesso VALUES (1,1);
INSERT INTO usuario_perfil_acesso VALUES (2,1);
GO




