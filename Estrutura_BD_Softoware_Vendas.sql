----------------------------------------------------------------
-- Verifcar se a erros e se a base de dados existe
----------------------------------------------------------------
use master;
go

-- Verifica se a base de dados já existe
if exists (select * from sys.databases where name = 'Software_Vendas_Pai')
begin
    alter database Software_Vendas_Pai set single_user with rollback immediate;
    drop database Software_Vendas_Pai;
end
go

----------------------------------------------------------------
-- Dar Inicio a Base de Dados 
----------------------------------------------------------------
-- Criar Base de Dados com o nome do Grupo
create database Software_Vendas_Pai;
go
-- Comando para usarmos/alterarmos a base de dados
use Software_Vendas_Pai;
go

----------------------------------------------------------------
--		 ... AQUI COMEÇAM A CRIAÇÃO DAS TABELAS ...
----------------------------------------------------------------	
--	Fase 1: Entidades, Entidades Fracas e Generalização
----------------------------------------------------------------
create table Encomenda(
    Numero_Encomenda int identity(1,1),
    Data_Encomenda date,
    Valor_Total money,
    Estado varchar(100),
    constraint pk_Encomenda primary key(Numero_Encomenda)
);

create table Linha_Encomenda(
    NE int,
    Linha_Encomenda int,
    Quantidade int not null,
    Descricao varchar(150),
    Preco money,
    Desconto decimal(5, 2),
    Imposto decimal(5, 2),
    constraint pk_Linha_Encomenda primary key(NE, Linha_Encomenda),
    constraint fk_Linha_Encomenda foreign key(NE) references Encomenda(Numero_Encomenda)
);

create table Tipo_Produto(
    Id_Produto varchar(10),
    Designacao varchar(50),
    Estado varchar(25),
    constraint pk_Id_Produto primary key(Id_Produto)
);

create table Material(
    Codigo varchar(15),
    Descricao varchar(150),
    Unidade_Venda varchar(10),
    Embalagem int,
    PVP_Unidade money,
    Stock int,
    constraint pk_Material primary key(Codigo)
);

create table Clientes(
	ID_Cliente varchar(15),
	Nome_Cliente varchar(100) NOT NULL,
	NIF varchar(20) NOT NULL,
	Morada_Completa varchar(200) NOT NULL,
	Codigo_Postal varchar(10),
	Cidade varchar(20),
	Email varchar(150) NOT NULL,
	Telefone varchar(150) NOT NULL,
    constraint pk_Clientes primary key(ID_Cliente)
);

create table Vendedores (
    ID_Vendedor int identity(1,1),
    Cargo varchar(50) default 'Vendedor',
    Nome varchar(100) NOT NULL,
    PIN varchar(4) NOT NULL, -- Código de acesso
    Email varchar(150) NULL, -- Novo: Pode ficar vazio (NULL) se não tiverem
    Senha varchar(50) NULL,
    Telemovel varchar(20) NULL, -- Novo
    Percentagem_Comissao decimal(5,2) default 5.00,
    Ativo bit default 1, -- Novo: 1 = Trabalha cá, 0 = Já saiu (não apagar histórico)
    constraint pk_Vendedores primary key(ID_Vendedor)
);

----------------------------------------------------------------	
--	Fase 2: M:N e associa��es c/mais do que 2 entidades
----------------------------------------------------------------

----------------------------------------------------------------	
--	Fase 3: 1:M e 1:1 (Alteração das tabelas existentes)
----------------------------------------------------------------

-- Encomenda feita por Cliente: Relação Faz
-- O modelo indica que Encomenda tem 'cliente' obrigatório (NOT NULL)
alter table Encomenda add 
	ID_Cliente varchar(15) not null,
	constraint fk_Encomenda_Cliente foreign key(ID_Cliente) references Clientes(ID_Cliente);

-- Linha de Encomenda possui Material: Relação Possui
-- O modelo indica que para cada linha de encomenda eiste um produto encomendado
alter table Linha_Encomenda add
    Codigo_Material varchar(15),
    constraint fk_Linha_Encomenda_Material foreign key(Codigo_Material) references Material(Codigo);

-- Cada Material tem um tipo de produto
-- O modelo indica que cada material possui um tipo de produto: Relação Tem
alter table Material add
    ID_Tipo varchar(10),
    constraint fk_Material_Produto foreign key(ID_Tipo) references Tipo_Produto(Id_Produto);

-- Cada vendedor faz uma encomenda
-- O modelo indica que cada vendedor pode fazer um ou mais encomendas: Relação Faz
alter table Encomenda add 
    ID_Vendedor int,
    constraint FK_Encomenda_Vendedor foreign key(ID_Vendedor) references Vendedores(ID_Vendedor);


-- Adiciona campo para o desconto global (ex: 2.5%)
alter table Encomenda 
add Desconto_Global decimal(5, 2) default 0;

-- Adicionar coluna para guardar o texto "50+10"
alter table Linha_Encomenda 
add Desconto_Texto VARCHAR(20);

-- Adiciona a taxa de IVA (padrão 23%)
alter table Material 
add Taxa_IVA decimal(5, 2) default 23.00;