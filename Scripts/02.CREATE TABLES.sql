USE [db_stock]
GO
/****** Object:  Table [dbo].[pedido]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedido](
	[id_pedido] [int] IDENTITY(1,1) NOT NULL,
	[dt_pedido] [datetime] NULL,
	[id_tipo_pedido] [int] NOT NULL,
	[vl_pedido] [decimal](14, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pedido_item]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pedido_item](
	[id_pedido_item] [int] IDENTITY(1,1) NOT NULL,
	[id_pedido] [int] NOT NULL,
	[cd_produto] [varchar](25) NULL,
	[cd_barra_sku] [varchar](25) NULL,
	[vl_item] [decimal](14, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pedido_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[perfil_de_acesso]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[perfil_de_acesso](
	[id_perfil] [int] NOT NULL,
	[nm_perfil] [varchar](100) NOT NULL,
	[ds_perfil] [varchar](100) NOT NULL,
	[cd_perfil_pai] [char](12) NULL,
 CONSTRAINT [pk_perfil_de_acesso] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto](
	[cd_produto] [varchar](25) NOT NULL,
	[nm_produto] [varchar](40) NOT NULL,
	[ds_composicao] [varchar](500) NULL,
	[cd_produto_colecao] [char](6) NULL,
	[cd_produto_grupo] [char](12) NULL,
	[cd_produto_subgrupo] [char](12) NULL,
	[id_produto_tipo] [int] NOT NULL,
	[cd_grade] [char](12) NULL,
	[cd_ncm] [varchar](8) NULL,
	[cd_cest] [varchar](8) NULL,
	[cd_extipi] [varchar](3) NULL,
	[cd_nve] [varchar](6) NULL,
	[cd_gtin_comercial] [varchar](14) NULL,
	[cd_unidade_comercial] [varchar](6) NULL,
	[cd_gtin_fiscal] [varchar](14) NULL,
	[cd_unidade_tributavel] [varchar](6) NULL,
	[dt_cadastro] [datetime] NULL,
	[dt_alteracao] [datetime] NOT NULL,
	[nm_fabricante] [varchar](100) NULL,
	[cd_ref_fabricante] [char](25) NULL,
 CONSTRAINT [pk_produto] PRIMARY KEY CLUSTERED 
(
	[cd_produto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_cor]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_cor](
	[cd_produto_cor] [char](12) NOT NULL,
	[nm_cor] [varchar](40) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_cor] PRIMARY KEY CLUSTERED 
(
	[cd_produto_cor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_estoque]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_estoque](
	[cd_produto_estoque] [bigint] IDENTITY(1,1) NOT NULL,
	[cd_filial] [char](12) NOT NULL,
	[cd_barra_sku] [varchar](25) NOT NULL,
	[nr_quantidade] [int] NOT NULL,
	[vl_unitario] [decimal](27, 2) NULL,
	[dt_criacao] [datetime] NULL,
	[dt_ultima_atualizacao] [datetime] NULL,
	[vl_custo_medio] [decimal](27, 2) NULL,
	[nr_quantidade_baixa] [int] NULL,
 CONSTRAINT [pk_produto_estoque] PRIMARY KEY CLUSTERED 
(
	[cd_produto_estoque] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_foto]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_foto](
	[id_produto_foto] [int] IDENTITY(1,1) NOT NULL,
	[cd_produto] [varchar](25) NOT NULL,
	[cd_barra_sku] [varchar](25) NULL,
	[ar_foto] [varchar](1) NULL,
	[nm_foto] [varchar](100) NULL,
	[nm_url] [varchar](100) NULL,
	[dt_inclusao] [datetime] NULL,
	[in_ativo] [char](1) NULL,
	[in_tipo] [char](1) NULL,
	[in_transparente] [char](1) NULL,
 CONSTRAINT [pk_produto_foto] PRIMARY KEY CLUSTERED 
(
	[id_produto_foto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_grade]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_grade](
	[cd_grade] [char](12) NOT NULL,
	[nm_grade] [varchar](60) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_grade] PRIMARY KEY CLUSTERED 
(
	[cd_grade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_grade_tamanho]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_grade_tamanho](
	[cd_grade_tamanho] [char](40) NOT NULL,
	[cd_grade] [char](12) NULL,
	[cd_produto_tamanho] [char](12) NULL,
 CONSTRAINT [pk_grade_tamanho] PRIMARY KEY CLUSTERED 
(
	[cd_grade_tamanho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_preco]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_preco](
	[cd_barra_sku] [varchar](25) NOT NULL,
	[cd_tabela] [char](4) NOT NULL,
	[vl_venda] [decimal](27, 2) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_preco] PRIMARY KEY CLUSTERED 
(
	[cd_barra_sku] ASC,
	[cd_tabela] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_sku]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_sku](
	[cd_barra_sku] [varchar](25) NOT NULL,
	[cd_produto] [varchar](25) NOT NULL,
	[cd_produto_cor] [char](12) NULL,
	[ds_cor] [varchar](60) NULL,
	[cd_produto_tamanho] [char](8) NULL,
	[ds_tamanho] [varchar](60) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_sku] PRIMARY KEY CLUSTERED 
(
	[cd_barra_sku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_tabela_preco]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_tabela_preco](
	[cd_tabela] [char](4) NOT NULL,
	[nm_tabela] [varchar](30) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_tabela_preco] PRIMARY KEY CLUSTERED 
(
	[cd_tabela] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_tamanho]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_tamanho](
	[cd_produto_tamanho] [char](12) NOT NULL,
	[nm_tamanho] [varchar](20) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_tamanho] PRIMARY KEY CLUSTERED 
(
	[cd_produto_tamanho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[produto_tipo]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[produto_tipo](
	[id_produto_tipo] [int] NOT NULL,
	[nm_tipo] [varchar](25) NULL,
	[in_ativo] [char](1) NULL,
 CONSTRAINT [pk_produto_tipo] PRIMARY KEY CLUSTERED 
(
	[id_produto_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nm_usuario] [varchar](100) NOT NULL,
	[nm_email] [varchar](100) NOT NULL,
	[ds_senha] [varchar](60) NOT NULL,
 CONSTRAINT [pk_usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuario_perfil_acesso]    Script Date: 27/03/2020 07:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario_perfil_acesso](
	[id_perfil] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
 CONSTRAINT [pk_usuario_perfil_acesso] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC,
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT (getdate()) FOR [dt_pedido]
GO
ALTER TABLE [dbo].[perfil_de_acesso] ADD  DEFAULT ('NULL') FOR [cd_perfil_pai]
GO
ALTER TABLE [dbo].[produto] ADD  CONSTRAINT [DF__produto__dt_cada__108B795B]  DEFAULT (getdate()) FOR [dt_cadastro]
GO
ALTER TABLE [dbo].[produto] ADD  CONSTRAINT [DF__produto__dt_alte__117F9D94]  DEFAULT (getdate()) FOR [dt_alteracao]
GO
ALTER TABLE [dbo].[produto_estoque] ADD  DEFAULT (getdate()) FOR [dt_criacao]
GO
ALTER TABLE [dbo].[produto_estoque] ADD  DEFAULT (getdate()) FOR [dt_ultima_atualizacao]
GO
ALTER TABLE [dbo].[produto_foto] ADD  CONSTRAINT [DF__produto_f__dt_in__1A14E395]  DEFAULT (getdate()) FOR [dt_inclusao]
GO
ALTER TABLE [dbo].[produto]  WITH CHECK ADD  CONSTRAINT [FK_produto_produto_tipo] FOREIGN KEY([id_produto_tipo])
REFERENCES [dbo].[produto_tipo] ([id_produto_tipo])
GO
ALTER TABLE [dbo].[produto] CHECK CONSTRAINT [FK_produto_produto_tipo]
GO
ALTER TABLE [dbo].[produto_estoque]  WITH CHECK ADD  CONSTRAINT [FK_produto_estoque] FOREIGN KEY([cd_barra_sku])
REFERENCES [dbo].[produto_sku] ([cd_barra_sku])
GO
ALTER TABLE [dbo].[produto_estoque] CHECK CONSTRAINT [FK_produto_estoque]
GO
ALTER TABLE [dbo].[produto_foto]  WITH CHECK ADD  CONSTRAINT [FK_produto_foto] FOREIGN KEY([cd_produto])
REFERENCES [dbo].[produto] ([cd_produto])
GO
ALTER TABLE [dbo].[produto_foto] CHECK CONSTRAINT [FK_produto_foto]
GO
ALTER TABLE [dbo].[produto_grade_tamanho]  WITH CHECK ADD  CONSTRAINT [FK_produto_grade_tamanho_produto_grade] FOREIGN KEY([cd_grade])
REFERENCES [dbo].[produto_grade] ([cd_grade])
GO
ALTER TABLE [dbo].[produto_grade_tamanho] CHECK CONSTRAINT [FK_produto_grade_tamanho_produto_grade]
GO
ALTER TABLE [dbo].[produto_grade_tamanho]  WITH CHECK ADD  CONSTRAINT [FK_produto_grade_tamanho_produto_tamanho] FOREIGN KEY([cd_produto_tamanho])
REFERENCES [dbo].[produto_tamanho] ([cd_produto_tamanho])
GO
ALTER TABLE [dbo].[produto_grade_tamanho] CHECK CONSTRAINT [FK_produto_grade_tamanho_produto_tamanho]
GO
ALTER TABLE [dbo].[produto_foto]  WITH CHECK ADD  CONSTRAINT [CK__produto_f__in_ti__1B0907CE] CHECK  (([in_tipo]='T' OR [in_tipo]='A' OR [in_tipo]='P'))
GO
ALTER TABLE [dbo].[produto_foto] CHECK CONSTRAINT [CK__produto_f__in_ti__1B0907CE]
GO
