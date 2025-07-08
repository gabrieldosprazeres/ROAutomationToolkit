# RO Automation Toolkit

[![Version](https://img.shields.io/badge/version-1.4.0-blue.svg)](https://github.com/gabrieldosprazeres/ro-automation-toolkit/releases)
[![License](https://img.shields.io/badge/license-Proprietária-red.svg)](LICENSE)

Ferramenta de automação para o jogo **Ragnarök Online**, permitindo o envio periódico de teclas para janelas específicas de client `ragexe.exe`.

![Screenshot do programa](https://i.imgur.com/LKPPryF.png)

## ✨ Funcionalidades

- Detecção automática de janelas `ragexe.exe` em execução;
- Envio automático de teclas (ex: F1, F2...) com intervalo personalizável;
- Criação, exclusão e gerenciamento de **perfis de configuração**;
- Interface com múltiplas abas e atalhos visuais (ícones);
- Suporte ao uso de múltiplas janelas do jogo;
- Interface leve e amigável.
- Aba **HotKey** em desenvolvimento para novos recursos futuros.

## 🚀 Como Usar

1. **Selecione uma janela do jogo**
   - Clique em "Atualizar lista" para carregar as janelas disponíveis
   - Selecione a janela desejada na lista
2. **Configure a tecla e intervalo**
   - Clique no campo "Tecla" e pressione a tecla desejada (ex: F1)
   - Defina o intervalo em milissegundos (mínimo: 100ms)
3. **Gerencie perfis (opcional)**
   - Selecione um perfil salvo ou digite um nome para novo perfil
   - Clique no ícone de disquete 💾 para salvar
   - Clique na lixeira 🗑️ para excluir
4. **Ative/desative o envio**
   - Clique em "Ativar" para iniciar o envio periódico
   - O botão muda para "Desativar" quando em operação
5. **Verifique os logs**
   - Todas as ações são registradas na caixa de log

## 📦 Estrutura do Projeto

```
ROAutomationToolkit/
├── .gitignore
├── app.manifest
├── CHANGELOG.md
├── LICENCE
├── Program.cs
├── README.md
├── ROAutomationToolkit.csproj
│
├── Forms/
│   ├── MainForm.cs
│   └── MainForm.Designer.cs
│
├── Models/
│   ├── Profile.cs
│   └── WindowInfo.cs
│
├── Services/
│   ├── KeySenderService.cs
│   ├── ProfileService.cs
│   └── WindowEnumerator.cs
│
├── resources/
│   ├── diskette.png
│   ├── garbage.png
│   ├── icon.ico
│   └── refresh.png
```

## 🚀 Como Buildar

Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado. Em seguida, execute os comandos abaixo no terminal (PowerShell):

```powershell
dotnet clean
Remove-Item -Recurse -Force bin, obj
dotnet restore
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

O executável final estará disponível em:

```
bin\Release\net10.0\win-x64\publish\
```

## 📋 Histórico de Versões

Confira o arquivo [CHANGELOG.md](CHANGELOG.md) para saber mais sobre o histórico de versões e melhorias.

## 🛡️ Licença

[LICENÇA DE USO – RO Automation Toolkit](LICENSE)

> Este software é **proprietário** e está liberado apenas para **uso pessoal, interno ou de demonstração**.  
> Para obter permissão comercial ou mais informações, entre em contato:

📧 gabrieldosprazeres@hotmail.com.br

---

© 2025 Gabriel dos Prazeres – Todos os direitos reservados.
