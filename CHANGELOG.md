# Changelog

All notable changes to this project will be documented in this file.

# [1.2.0] - 07/07/2025

### Added

- **Novo serviço de envio de teclas (`KeySenderService`)**:
  - Implementação de envio de teclas via mensagens Windows (`PostMessage`)
  - Suporte para WM_KEYDOWN/WM_KEYUP
  - Sistema de eventos para logs e controle de estado
  - Thread dedicada para envio assíncrono de teclas
- **Mecanismo de seleção de tecla configurável**:
  - Suporte para múltiplas teclas via `SetKey()`
  - Validação de intervalo mínimo (100ms)
- **Mecanismo de seleção de tecla configurável**:
  - `LogMessage` para feedback em tempo real
  - `SendingStateChanged` para controle de estado

### Changed

- **Refatoração do MainForm**:
  - Adição de suporte completo ao `KeySenderService`
  - Integração do serviço com seleção de janelas
  - Aprimoramento de tratamento assíncrono
- **Atualização da interface**:
  - Novo layout com controles de envio de teclas
- **Melhorias no serviço existente**:
  - Adição de validação reforçada para handles de janela
  - Otimização do sistema de threads

### Fixed

- Correção de possível race condition no loop de envio
- Tratamento de exceções para caminho de ícone inválido
- Prevenção de memory leak no carregamento de imagens
- Sincronização de thread na atualização da UI

### Removed

- Redundância na inicialização de componentes
- Código obsoleto de versões anteriores

## [1.1.0] - 07/07/2025

### Added

- Implementação do sistema de detecção de janelas do Ragexe
- Novo serviço `WindowEnumerator` para enumeração de processos
- Novo modelo `WindowInfo` para armazenar dados de janelas
- Interface de seleção de janelas com ComboBox e botão de refresh
- Operações assíncronas para carregamento de janelas
- Novo recurso gráfico (`refresh.png`) para o botão de atualização
- Tratamento de exceções durante coleta de informações de processos
- Suporte para ordenação de janelas por tempo de criação

### Changed

- **Refatoração completa da estrutura de pastas**:
  - Pasta `Forms` para componentes de UI
  - Pasta `Services` para lógica de negócios
  - Pasta `Models` para estruturas de dados
- Redimensionamento do formulário principal (350x300 → 375x300)
- Atualização do título da janela para "RO Automation Toolkit v1.1.0"
- Substituição de operações síncronas por versões assíncronas
- Otimização do processo de carregamento de janelas com threads separadas

### Fixed

- Tratamento robusto para processos inacessíveis
- Prevenção de vazamento de handles durante enumeração
- Ajuste no DPI scaling (PerMonitorV2)
- Corrigido comportamento do ComboBox com muitos itens

### Removed

- Removida inicialização redundante de componentes
- Eliminado código duplicado na disposição de controles

## [1.0.0] - 06/07/2025

### Added

- Release inicial do RO Automation Toolkit
- Formulário principal com layout básico
- Sistema de ícones e manifest
- Configuração básica de projeto Windows Forms
- Suporte inicial para HighDPI
