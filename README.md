# Model Compiler

## Executive Summary

The repository has been forked from [OPCFoundation/UA-ModelCompiler][ModelCompiler] and will be synchronized with the origin repository occasionally.

The main goal is to

- work on [OPC UA Information Models Compliance Testing][ASMDComplianceTesting]
- recover files required by the [OPCFoundation/UA-ModelCompiler][ModelCompiler] from the files provided by the origin.

The [OPC Foundation](https://opcfoundation.org) `Model Compiler` will generate C# and ANSI C source code from XML files which include the UA Services, data-types, error codes, etc.; and numerous CSV files that contain NodeIds, error codes, and attributes etc.

The input format for the tool is a file that conforms to the schema defined in `UA Model Design.xsd`.

The output of the tool includes:

 1. A NodeSet which conforms to the schema defined in Part 6 Annex F;
 2. An XSD and BSD (defined in Part 3 Annex C)  that describes any datatypes;
 3. Class and constant definitions suitable for use with the .NET sample libraries;
 4. Other data files used to load an information model into a Server built with the .NET sample libraries;
 5. A CSV file which contains numeric identifiers.

The [UA Model Design.xsd](https://github.com/OPCFoundation/UA-ModelCompiler/blob/master/ModelCompiler/UA%20Model%20Design.xsd) has more information about the schema itself.

The .NET sample libraries has [a sample Model Design file](https://github.com/OPCFoundation/UA-.NET/blob/master/SampleApplications/Samples/Common/Sample/SampleDesign.xml) that illustrate how to create a user defined model.

This [batch file](https://github.com/OPCFoundation/UA-.NET/blob/master/SampleApplications/Samples/Common/BuildDesign.bat) is used to regenerate the files used in the sample after changes.

The tool only produces ANSI C output for the stack.

All of the standard outputs are published in the [Nodeset GitHub repository](https://github.com/OPCFoundation/UA-Nodeset)

Developers should never need to build the standard outputs themselves.

## About this Repository

> NOTE: In this repository, the default branch has been changed to `main`.  The `master` branch is preserved for synchronization with the origin only. Further versioning will be implemented using new `tags` instead of branches. New branches are created only for development purposes.


This repository is updated directly. 


## License Model

The ModelCompiler code is [MIT license](https://github.com/mpostol/UA-ModelCompiler/blob/main/license.md), however, it links to the UA-.NETStandard NuGet packages which is covered under the [OPC Foundation Redistributables licence](https://opcfoundation.org/license/redistributables/1.3/index.html). If a user chooses the version that links directly to the  UA-.NETStandard submodule then the license the UA-.NETStandard [dual license](https://github.com/OPCFoundation/UA-.NETStandard/blob/master/LICENSE.txt) applies.
  
## Docker Build

The compiled version of this `Model Compiler` is available in DockerHub as [sailavid/ua-modelcompiler](https://cloud.docker.com/u/sailavid/repository/docker/sailavid/ua-modelcompiler).
TODO: Change URL to use official OPCF Docker container!

If you like to build your own container, just use the provided Dockerfile in this repo.

We assume you have your `myModel.xml` and `myModel.csv` on your host system in `$HOME/myModel`. If not change the path in the next command.

The Docker container uses the model compiler executable as entry point.
This means that any parameter you pass to the `docker run` command will be directly passed to the model compiler executable.

This is an examplary call to docker run:

```bash
docker run \
  --mount type=bind,source=$HOME/myModel,target=/model \
  sailavid/ua-modelcompiler -- \
   -console -d2 /model/myModel.xml -cg /model/myModel.csv -o2 /model/Published/my_model
```

Note that here we are binding `$HOME/myModel` to the path `/model` inside the container.

You need to make sure that the output directory already exists before running this command.

The expected output is:

``` txt
Trying file: /model/OpcUaDiModel.xml
Trying file: ./Design/OpcUaDiModel.xml
Trying file: /model/OpcUaDiModel.csv
Trying file: ./Design/OpcUaDiModel.csv
```

Note, there's no final success message.

To use the `PublishModel.sh` script you can also override the entrypoint. The PublishModel script is a wrapper around the model compiler executable and is handling directory creation and copying the original model files.

```bash
docker run \
  --mount type=bind,source=$HOME/myModel,target=/model \
  --entrypoint "/app/PublishModel.sh" \
  sailavid/ua-modelcompiler \
   /model/myModel my_model /model/Published
```

The expected output is:

```TXT
Building Model for 'my_model'
/app/Opc.Ua.ModelCompiler.exe -console -d2 /model/myModel.xml -cg /model/myModel.csv -o2 /model/Published/my_model
Trying file: /model/OpcUaDiModel.xml
Trying file: ./Design/OpcUaDiModel.xml
Trying file: /model/OpcUaDiModel.csv
Trying file: ./Design/OpcUaDiModel.csv
Copying Model files to /model/Published/my_model

```

## Example Generation

The following process will demonstrate how to generate code using the supplied nodeset files:

 1. Clone the repository and then build the source in Visual Studio 2015, in Release mode.
 2. Open a Command prompt and then launch the BuildStandardTypes.bat
 3. After the script completes, navigate to the .\Published folder to view the output.
 4. Optionally, modify the BAT file and specify the location of your UA Stack(s) to automatically copy the generated files.

### XML Files

#### Model Design example

An excerpt of the Model Design file is shown here:

```txt
?xml version="1.0" encoding="utf-8" ?>
<opc:ModelDesign
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xmlns:xsd="http://www.w3.org/2001/XMLSchema"
  xmlns:opc="http://opcfoundation.org/UA/ModelDesign.xsd"
  xmlns:ua="http://opcfoundation.org/UA/"
  xmlns="http://opcfoundation.org/UA/"
  xmlns:uax="http://opcfoundation.org/UA/2008/02/Types.xsd"
  TargetNamespace="http://opcfoundation.org/UA/"
  TargetNamespaceVersion="1.02"
>
  <opc:Namespaces>
    <opc:Namespace Name="OpcUa" Prefix="Opc.Ua" InternalPrefix="Opc.Ua.Server" XmlNamespace="http://opcfoundation.org/UA/2008/02/Types.xsd">http://opcfoundation.org/UA/</opc:Namespace>
  </opc:Namespaces>

  <opc:Property SymbolicName="NodeVersion" DataType="ua:String" PartNo="3">
    <opc:Description>The version number of the node (used to indicate changes to references of the owning node).</opc:Description>
  </opc:Property>

  <opc:Property SymbolicName="ViewVersion" DataType="ua:UInt32" PartNo="3">
    <opc:Description>The version number of the view.</opc:Description>
  </opc:Property>
```

## Other Repositories

This `ModelCompiler` is used to generate the content of the [Nodeset GitHub repository](https://github.com/OPCFoundation/UA-Nodeset).

## Partnership program

I am a researcher and University associate who is passionate about applying knowledge and experience in building a Machine to Machine (M2M) meaningful interoperability based on OPC UA. Let's build it with you and for you. To join our effort and create an organizational context I have launched an open-access **Object-Oriented Internet Partnership Program**. Hence, maintenance of this repository and further development of the OPC UA Information Model Domain-Specific Language will be carried out under a broader concept described in the following article

[Object-Oriented Internet Partnership Program][Sponsorship]

**Consider joining**. Visit the section [How to be involved][SponsorshipToBeInvolved] to get more. I hope that thanks to this partnership program we will establish long-term mutually beneficial cooperation. Your participation is needed to make sure that the work will continue as expected. As a rule of thumb, the work priority is derived from community feedback.

I strongly encourage community participation and contribution to this project. First, please fork the repository and commit your changes there. Once happy with your changes you can generate a `pull request`.

When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

Please note we have a code of conduct, please follow it in all your interactions with the project.

## See Also

- [OPC UA Information Models Compliance Testing][ASMDComplianceTesting]
- [Address Space Prototyping Tool (asp.exe)][ASP]
- [Object-Oriented Internet Partnership Program][Sponsorship]
- [How to be involved][SponsorshipToBeInvolved]
- [OPCFoundation/UA-ModelCompiler][ModelCompiler]
- [OPC UA NodeSets and Other Supporting Files][UANodeset]
- [Official OPC UA .NET Standard Stack from the OPC Foundation][NETStandard]
- [OPC UA Makes Machine-Centric Global Village Possible – Call for Sponsors](https://mpostol.wordpress.com/2020/01/03/opc-ua-makes-machine-centric-global-village-possible-call-for-sponsors/)
- Tutorial by Stefan Profanter [here](https://opcua.rocks/custom-information-models/).

Join me at [LinkedIn](https://www.linkedin.com/in/mpostol/)

[NETStandard]: https://github.com/OPCFoundation/UA-.NETStandard#official-opc-ua-net-standard-stack-from-the-opc-foundation
[ASMDComplianceTesting]: https://mpostol.github.io/ASMD/ASMD50-ModelsTesting
[Sponsorship]: https://github.commsvr.com/AboutPartnershipProgram.md.html
[SponsorshipToBeInvolved]: https://github.commsvr.com/AboutPartnershipProgram.md.html#how-to-be-involved
[ModelCompiler]: https://github.com/OPCFoundation/UA-ModelCompiler
[UANodeset]: https://github.com/OPCFoundation/UA-Nodeset#opc-ua-nodesets-and-other-supporting-files
[ASP]: https://commsvr.gitbook.io/ooi/semantic-data-processing/addressspacecompliancetesttool
