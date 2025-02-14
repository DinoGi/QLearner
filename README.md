QLearner | Born on July 22, 2013 | Lives at www.pftq.com/qlearner/ 
© 2013 QLearner by pftq

"A practical, generalized application of various machine learning algorithms for C# programming."

QLearner provides generalized machine learning algorithms to solve to any problem defined by a QState.  The Plugins Template download includes a template to build your own QStates and several examples to learn from.  For more advanced users, new QAlgos can also be created to leverage the framework provided by QLearner.  Documentation and API are provided for developing your own plugins, so that it is not necessary to actually modify the source code of the underlying QLearner framework.  Additionally, QLearner can be compiled or included as a class library for instantiating instances of QLearner's algos for your own code projects.

To use QLearner, simply open the QLearner program and click on the QState box to select a problem to solve and the QAlgo box to choose an algorithm to solve it with.  Press Learn to give QLearner a chance to "practice" at the problem and learn about it.  Press Awaken for QLearner to apply its most current knowledge to solve the problem in the most efficient way it currently knows how.  To use QLearner programatically, include QLearner as a resource in your project and instantiate QAgent objects with instances of QState and QAlgo through QAgent's Learn/Awaken methods.

QLearner can be made to perpetually learn and continue solving an ongoing problem (such as world domination, the stock market, or the weather) if the QState Plugin is designed in such a way such that the IsEnd() state is never reached.

About QLearner:

QLearner was originally a short AI script built into Tech Trader for optimizing stock trading strategies.  I went ahead and took the script out of Tech Trader and made QLearner a stand-alone program so that it has more flexibility to work besides just strategy optimization for stocks.  Namely, it can now apply machine learning to any situation the user builds a template for (or finds a template for...).
	
Like the name suggests, QLearner originally used Q-Learning algorithms but has since incorporated numerous other concepts to behave and learn in a more human-like manner.  Details on my approach to machine learning can be found in my write-up Creating Sentient Artificial Intelligence:
http://www.pftq.com/blabberbox/?blogcat=writings;page=Creating_Sentient_Artificial_Intelligence

-pftq


Credits:

	* PriorityQueue by BlueRaja @ Bitbucket for fast heap implementation.


Changelog:

	v1-3 2014-09-06 to 2015-07-18, pftq:

	* Converted features, actions to abstractions to allow any datatype as the key.
	* Added new GetObservedStates method to QState to allow observation of others' actions or choices it could have made.
	* Added improved QLearning algo called QLearning_Observant that takes advantage of GetObservedStates. Exponentially improves learning rate.
	* Performance improvements by less frequent GUI updates.

	v1-2 2014-07-07 to 2014-08-01, pftq:

	* Added maze QState for basic maze solving tests.
	* Finished Approximate QLearning algo.
	* Modulized algos, so that new algos can be easily added via template/API.
	* Modulized QAgent class so that QLearner can be included as C# library and run programatically.
	* Added various search algorithms to the Resources namespace.
	* Overhauled plugin system to allow multiple QStates and QAlgos per file.
	* Added ability to load and save learned data.
	* Added score and avg score meters.

	v1-1 2013-07-24, pftq:

	* Added features construct for multifactoral analysis of states.

	v1-0 2013-07-22 to 07-23, pftq:

	* QLearner is born! It has figured out how to count to 100... and pretty much any other state-based problem.
