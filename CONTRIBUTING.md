# Contributing Guidelines

The `F0.Minesweeper` open source project is hosted on **GitHub**, so we start this document off with the [GitHub Community Guidelines](https://help.github.com/en/github/site-policy/github-community-guidelines).

## Coding Style
See [.editorconfig](./.editorconfig) file.

See also
* [EditorConfig.org](https://editorconfig.org/)
* [Visual Studio Docs](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options)

## Development Techniques
Apply _test-driven development_ when adding features or fixing bugs.
Follow the rules of _Clean Code_ for the entire codebase.

## Version Scheme
See [Semantic Versioning](https://semver.org/).

Although games do not declare a public API, `MAJOR`, `MINOR` and `PATCH` changes for each release are observed from the player's point of view.

## Issues
See [GitHub Guides: Issues](https://guides.github.com/features/issues/).

Each _issue_ should be submitted with an intention-revealing _title_ and a descriptive _comment_.
Define the scope of an _issue_ by applying meaningful _labels_ rather than prefixing the _title_.
Before work is started, the _issue_ should have contributing _assignees_ added or updated.

_Issues_ that result in changes to the repository are closed via [pull requests](#pull-requests).

## Branching Workflow
Based on the [GitHub flow](https://guides.github.com/introduction/flow/).

The [main](https://github.com/Flash0ver/F0.Minesweeper) branch is the _default_ branch.
It includes all verified changes being published in the next release.
New _topic branches_ are created off of `main`.
Tags target the `main` branch.

The [publish](https://github.com/Flash0ver/F0.Minesweeper/tree/publish) branch is the second long-running branch.
It represents the state of the _latest release_.
On each release published, the `publish` branch is updated from `main` and pushed to.

Every change to the repository is committed to short-lived _topic branches_.
New _topic branches_ are based on `main`.
Their work in progress is status checked, reviewed and discussed through [pull requests](#pull-requests).
When approved, the _topic branch_ may be merged into the `main` branch while maintaining a linear history.
After a successful merge, the topic branch should be deleted.

Topic branch naming conventions:
* all lowercase
* start with `commit type` (see [Commit Messages](#commit-messages))
* followed by a _slash_: `/`
* end with a short description, use _hyphen_ as word divider: `-`

Format: `{type}/{short-description}`\
Example: `docs/contributing-guidelines`

## Commit Messages
Based on [Conventional Commits](https://www.conventionalcommits.org/),
where a _major_/_breaking change_ is indicated by a `!` between the `type/scope` and the `:`,
and may optionally include a `BREAKING CHANGE:` footer with additional information.

Another common footer is `Co-authored-by: name <name@example.com>` to [attribute a commit to more than one author](https://help.github.com/en/github/committing-changes-to-your-project/creating-a-commit-with-multiple-authors).
Such _trailers_ should be provided after the `BREAKING CHANGE:` footer.

Write both the _description_ and the optional _body_ of commit messages in _present tense imperative_.

For [pull requests](#pull-requests)' squashed merge commit messages, append the _Pull-Requests-Number_ to the end of the _commit title_ surrounded by parentheses.

Format:
```
<type>[optional scope][!]: <description> [optional pull request number]

[optional body]

[optional footer(s)]
```
Example:
```
build(game-logic)!: change language version to C# 9.0 (#123)

change entities to record types
use covariant return types

BREAKING CHANGE: drop support for .NET Core 3.1
Co-authored-by: name <name@example.com>
```

## Pull Requests
See [GitHub Help: Creating a pull request](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).

The _title_ of a _pull request_ should be the [commit message](#commit-messages)'s _commit title_ of the closing *squash and merge*.

_Pull requests_ that resolve one or many [issues](#issues) should reference these by [linking a pull request to an issue using a supported keyword in the pull request's description](https://help.github.com/en/github/managing-your-work-on-github/linking-a-pull-request-to-an-issue#linking-a-pull-request-to-an-issue-using-a-keyword),
such as `Closes #123`.

## Changelog
Based on [Keep a Changelog](https://keepachangelog.com/),
where the `Unreleased` section is called `vNext`,
and where changes are grouped by product scope,
and where changes within a group begin with a _type_.

You can find the [CHANGELOG.md](./CHANGELOG.md) in the root of the repository.
